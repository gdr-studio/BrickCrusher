using System;
using System.Collections.Generic;
using UnityEngine;

namespace Merging
{
    [DefaultExecutionOrder(-1)]
    public class CannonPlacementManager : MonoBehaviour
    {
        private static CannonPlacementManager _instance;

        public static CannonPlacement GetOne() => _instance.GetItem();
        public static void ReturnOne(CannonPlacement cannon) => _instance.ReturnItem(cannon);
        public static PlacementBuySign GetBuySign() => _instance.GetSign();
        public static void ReturnBuySign(PlacementBuySign sign) => _instance.ReturnSign(sign);
        
        [SerializeField] private PlacementBuySign _signPrefab;
        [SerializeField] private CannonPlacement _placementPrefab;
        [SerializeField] private int _preSpawnCount = 4;
        private PlacementBuySign _sign;

        private Queue<CannonPlacement> _placements;
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                Init();
            }
        }
        
        public void Init()
        {
            _placements = new Queue<CannonPlacement>();
            for (int i = 0; i < _preSpawnCount; i++)
            {
                var instance = Instantiate(_placementPrefab, transform);
                _placements.Enqueue(instance);
                instance.gameObject.SetActive(false);
            }
            _sign = Instantiate(_signPrefab, transform);
            _sign.Hide();
        }

        private CannonPlacement GetItem()
        {
            return _placements.Dequeue();
        }

        private void ReturnItem(CannonPlacement cannon)
        {
            cannon.Hide();
            _placements.Enqueue(cannon);   
        }

        
        private void ReturnSign(PlacementBuySign sign)
        {
            _sign = sign;
        }


        private PlacementBuySign GetSign()
        {
            return _sign;
        }

    }
}