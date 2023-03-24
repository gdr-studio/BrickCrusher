using DG.Tweening;
using UnityEngine;

namespace Tutorial.Hand
{
    public class TutorHand : MonoBehaviour
    {
        public float scaleDownTime = 0.1f;
        public float scaleBackTime = 0.1f;
        public float downScale = 0.85f;
        public float normalScale = 0.85f;
        public float moveSpeed = 10;
        public Ease scaleEase;
        public Ease moveEase;
        public float clickDownDuration = 0.5f;
        public float clickDownDelay = 0.5f;
        [Space(20)] 
        [SerializeField] private Transform _hand;
        private Sequence _handSeq;
        
        public void Show()
        {
            _hand.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _hand.gameObject.SetActive(false);   
        }

        public void ShowClickAt(Vector2 position)
        {
            _handSeq?.Kill();
            _handSeq = DOTween.Sequence();
            _hand.position = position;
            _handSeq.Append(_hand.DOScale(Vector3.one * downScale, clickDownDuration))
                .Append(_hand.DOScale(Vector3.one , clickDownDuration))
                .SetDelay(clickDownDelay)
                .SetLoops(-1);
           
        }
        
        public void ShowToDrag(Vector2 startPos, Vector2 endPos)
        {
            _handSeq?.Kill();
            _handSeq = DOTween.Sequence();
            Reset();
            var moveTime = (endPos - startPos).magnitude / moveSpeed * 0.01f;
            _handSeq.Append(_hand.DOScale(Vector3.one * downScale, scaleDownTime).SetEase(scaleEase))
                .Append(_hand.DOMove(endPos, moveTime).SetEase(moveEase))
                .Append(_hand.DOScale(Vector3.one * normalScale, scaleBackTime).SetEase(scaleEase).OnComplete(() =>
                {
                    Reset();
                }) ).SetLoops(-1);
            
            void Reset()
            {
                _hand.position = startPos;
                _hand.localScale = Vector3.one * normalScale;
            }
        }

        public void StopAndHide()
        {
            _handSeq?.Kill();
            Hide();
        }

    }
}