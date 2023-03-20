using System.Collections;
using System.Collections.Generic;
using Game.View.Impl;
using Helpers;
using UnityEngine;
using Zenject;

namespace Services.ScalePieceDown
{
    public class ScalePieceDownService
    {
        private List<TimedData> _timedData = new List<TimedData>();
        private HashSet<TimedData> _removeQueue = new HashSet<TimedData>();

        private float _scaleDuration = 0.56f;
        
        [Inject] private ICoroutineService _coroutineService;
        private bool _started = false;
        private Coroutine _scaling;
        private Coroutine _removing;

        public bool Allowed = false;

        public void StopAndClearAll()
        {
            if(_scaling != null)
                _coroutineService.StopCor(_scaling);
            if(_removing != null)
                _coroutineService.StopCor(_removing);
            _timedData.Clear();
            _removeQueue.Clear();
            _removing = null;
            _scaling = null;
            _started = false;
        }
        
        public void Schedule(BreakablePart.BrokenPiece piece, float delay)
        {
            if (Allowed == false)
                return;
            
            if (_started == false)
            {
                _scaling = _coroutineService.StartCor(Scaling());
                _removing = _coroutineService.StartCor(Removing());
                _started = true;
            }
            var timedData = new TimedData()
            {
                piece = piece,
                delay = delay,
                startScale = piece.Rb.transform.localScale.x,
                elapsedScaling = 0
            };
            _timedData.Add(timedData);
        }

        private IEnumerator Scaling()
        {
            while (true)
            {
                var dt = UnityEngine.Time.deltaTime;
                foreach (var data in _timedData)
                {
                    if (data.delay > 0)
                    {
                        data.delay -= dt;
                        // Debug.Log($"delay: {data.delay}");
                        if (data.delay < 0)
                        {
                            data.piece.Rb.isKinematic = true;
                            data.piece.Coll.enabled = false;
                        }
                        continue;
                    }

                    data.elapsedScaling += dt;
                    data.piece.Rb.transform.localScale = Vector3.one * Mathf.Lerp(data.startScale, 0f, data.elapsedScaling / _scaleDuration);
                    // Debug.Log($"scale: {Mathf.Lerp(1f, 0f, data.elapsedScaling / _scaleDuration)}");
                    if (data.elapsedScaling > _scaleDuration)
                        _removeQueue.Add(data);
                }
                yield return null;
            }
    
        }

        
        private IEnumerator Removing()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                foreach (var data in _removeQueue)
                {
                    data.piece.Rb.gameObject.SetActive(false);
                    _timedData.Remove(data);
                }
                _removeQueue.Clear();
                yield return null;
            }
        }

        


        private class TimedData
        {
            public BreakablePart.BrokenPiece piece;
            public float delay;
            public float elapsedScaling;
            public float startScale;
        }
    }
    
}