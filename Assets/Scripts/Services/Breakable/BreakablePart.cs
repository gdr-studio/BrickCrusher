using System.Collections.Generic;
using Services.Parent;
using Services.ScalePieceDown;
using UnityEngine;
using Zenject;

namespace Game.View.Impl
{
    public partial class BreakablePart : MonoBehaviour
    {
        public Transform root;
        public GameObject _mainPart;
        public bool parentToShards;
        public float scaleDownTime = 1f;
        [SerializeField] private List<BrokenPiece> _parts = new List<BrokenPiece>();
        [Inject] private IParentService _parentService;
        [Inject] private ScalePieceDownService _scalePieceDownService;

        // ReSharper disable Unity.PerformanceAnalysis
        public void GetParts()
        {
            if (root == null)
                root = transform;
            if (_mainPart == null)
                _mainPart = gameObject;
            _parts.Clear();
            for (int i = 0; i < root.childCount; i++)
            {
                var go = root.GetChild(i).gameObject;
                var coll = go.GetComponent<Collider>();
                var rb = go.GetComponent<Rigidbody>();
                if (coll != null && rb != null)
                {
                    var data = new BrokenPiece(rb, coll);
                    _parts.Add(data);
                }
            }   
        }

        public void ActivateAndPush(Vector3 force)
        {
            _mainPart.SetActive(false);
            var par = transform;
            if (parentToShards)
                par = _parentService.ShardsParent;
            else
                par = _parentService.DefaultParent;
            foreach (var part in _parts)
            {
                part.Reparent(par);
                part.Push(force);   
                _scalePieceDownService.Schedule(part, scaleDownTime);
            }
        }
        
        public void ActivateAndPushFromCenter(Vector3 force, Vector3 centerPosition, float centerForce)
        {
            _mainPart.SetActive(false);
            var par = transform;
            if (parentToShards)
                par = _parentService.ShardsParent;
            else
                par = _parentService.DefaultParent;
            
            foreach (var part in _parts)
            {
                var f = (part.Rb.position - centerPosition) * centerForce;
                part.Reparent(par);
                part.Push(force + f);   
                _scalePieceDownService.Schedule(part, scaleDownTime);
            }
        }

        public void SetPassive()
        {
            foreach (var part in _parts)
            {
                part.Active = false;
            }   
            _mainPart.SetActive(true);
        }
    }
}