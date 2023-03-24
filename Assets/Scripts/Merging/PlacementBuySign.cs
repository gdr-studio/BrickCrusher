using LittleTricks;
using TMPro;
using UnityEngine;

namespace Merging
{
    public class PlacementBuySign : MonoBehaviour
    {
        public TweenScaler scaler;
        public CannonPlacementCost costs;
        public TextMeshPro costText;
        public int cost = 200;
        private Camera _cam;
        
        public void Show()
        {
            _cam = Camera.main;
            transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
            cost = costs.GetNextCost();
            costText.text = $"{cost}$";
            scaler.StartScalingLoop();
            gameObject.SetActive(true);
            
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
        }
    }
}