﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Weapons;

namespace Merging
{
    public partial class MergingManager : MonoBehaviour
    {
        public static event Action OnSpawned;
        
        private static MergingManager _instance;
        [SerializeField] private List<MergingItemArea> _areas;
        [SerializeField] private LayerMask _cannonsMask;
        [SerializeField] private MergingPurchaser _purchaser;
        [SerializeField] private MergingLogic _mergingLogic;
        [SerializeField] private GraphicRaycaster _raycaster;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private PlayerWeaponChannel _weaponChannel;
        [Space(20)]
        private PointerEventData _pointerEventData;
        public Image movingImage;
        private MovingData _movingData;
        private Camera _camera;

        private bool _isEnabled;
        public static bool IsEnabled
        {
            get => _instance._isEnabled;
            set => _instance._isEnabled = value;
        }
        public static void Refresh() => _instance.RefreshMerging();
        
        private void Awake()
        {
            if(_instance == null)
                _instance = this;
            _purchaser.areas = _areas;
            _movingData = new MovingData();
            _movingData.movable = movingImage.transform;
            _movingData.image = movingImage;
            movingImage.enabled = false;
            IsEnabled = true;
            _camera = Camera.main;
        }

        private void RefreshMerging()
        {
            foreach (var area in _areas)
            {
                area.SetEmpty();
            }   
            _purchaser.CheckPurchasable();
            _weaponChannel.SpawnedCount.Val = 0;
        }
        
        private void Update()
        {
            if (_isEnabled == false)
                return;
            if (Input.GetMouseButtonDown(0))
            {
                var results = RaycastUI();
                ProcessUIOnClick(results);
            }   
            else if (Input.GetMouseButtonUp(0))
            {
                var results = RaycastUI();
                ProcessDrop(results);
            }
            if (Input.GetMouseButton(0))
            {
                if (_movingData.data == null)
                    return;
                Move();
                CheckMouseOver();
            }
        }

        private void ProcessUIOnClick(List<RaycastResult> results)
        {
            foreach (var result in results)
            {
                var area = result.gameObject.GetComponent<MergingItemArea>();
                if (area != null)
                {
                    PickFrom(area);
                    return;
                }
            }
            if (TryCastBuySign())
                return;
            TryRemoveCannon();
        }

        private void ProcessDrop(List<RaycastResult> results)
        {
            if (_movingData.data == null)
                return;
            if (results.Count == 0)
            {
                DropNoUI();
                return;
            }
            foreach (var result in results)
            {
                var area = result.gameObject.GetComponent<MergingItemArea>();
                if (area != null)
                {
                    DropAt(area);
                    return;
                }
                DropAt(null);
            }   
        }

        private void DropNoUI()
        {
            if (IsOverUI())
            {
                DropAt(null);
            }
            else
            {
                if(_movingData.IsSpawned)
                    FinalSpawn();
                else
                    DropAt(_movingData.fromArea);                    
            }   
        }

        private void TryRemoveCannon()
        {
            var results = RaycastWorld(_cannonsMask);
            if (results.Length == 0)
                return;
            foreach (var result in results)
            {
                var cannon = result.collider.gameObject.GetComponent<CannonSpawnable>();
                if (cannon != null)
                {
                    _weaponChannel.CallRemoveCannon(cannon);
                    cannon.MergingArea.SetDataBack();
                    cannon.MergingArea.PlayReturnEffect();
                    // _purchaser.ReturnMoney(cannon.cannonName);
                }
            }
        }

   
        
        private bool TryCastBuySign()
        {
         
            var results = RaycastWorld(_cannonsMask);
            if (results.Length == 0)
                return false;
            foreach (var result in results)
            {
                var buySign = result.collider.gameObject.GetComponent<PlacementBuySign>();
                if (buySign != null)
                {
                    _weaponChannel.BuyPlacement.Invoke();
                    return true;
                }
            }
            return false;
        }

        private void CheckMouseOver()
        {
            // IS on UI
            if (IsOverUI())
            {
                if (_movingData.IsSpawned)
                    StopTracking();
                return;
            }
            if (_weaponChannel.CanSpawn() == false)
                return;
            Track();
        }

        
        private void Track()
        {
            var can = _weaponChannel.CheckSpawnAvailable.Invoke();
            if (can)
            {
                _weaponChannel.Track.Invoke(_movingData.data, _movingData.fromArea);
            }
            _movingData.IsSpawned = can;
        }

        private void FinalSpawn()
        {
            _weaponChannel.StopTacking.Invoke(true);   
            _movingData.fromArea.SetSpawned();
            _movingData.HideAndClear();
            OnSpawned?.Invoke();
        }

        private void StopTracking()
        {
            _weaponChannel.StopTacking.Invoke(false);   
            _movingData.IsSpawned = false;
        }
        
        private bool IsOverUI()
        {
            var results = RaycastUI();
            return results.Count > 0;
        }
        
        private List<RaycastResult> RaycastUI()
        {
            _pointerEventData = new PointerEventData(_eventSystem);
            _pointerEventData.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            _raycaster.Raycast(_pointerEventData, results);
            return results;
        }

        private RaycastHit[] RaycastWorld(LayerMask mask)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            var results = Physics.RaycastAll(ray, 200, mask);
            return results;
        }

        private void PickFrom(MergingItemArea area)
        {
            if (area.IsTaken)
                return;
            var otherData = area.currentData;
            if (otherData == null)
            {
                _purchaser.TryPurchase(area);
                _purchaser.CheckPurchasable();
                return;
            }
            _movingData.fromArea = area;
            _movingData.Show(otherData);
            _movingData.movable.transform.position = Input.mousePosition;
            area.TakeFrom();
        }

        private void DropAt(MergingItemArea area)
        {
            // Dbg.Red("Drop at");
            if (_movingData.data == null)
            {
                _purchaser.CheckPurchasable();
                return;
            }
            StopTracking();
            if (_movingData.IsSpawned)
            {
                _movingData.HideAndClear();
                _purchaser.CheckPurchasable();
                return;
            }
            if (area == null)
                _movingData.fromArea.SetData(_movingData.data);
            else
            {
                if (area.IsEmpty()) // empty cell
                {
                    area.SetData(_movingData.data);
                    _movingData.fromArea.SetEmpty();
                }
                else
                {
                    if (area == _movingData.fromArea) // same cell
                    {
                        area.SetData(_movingData.data);
                    }
                    else // different cell
                    {
                        var merged = _mergingLogic.Merge(_movingData.data, area);
                        if (!merged)
                            _movingData.fromArea.SetData(_movingData.data);
                        else
                            _movingData.fromArea.SetEmpty();        
                    }
                }
            }
            _movingData.HideAndClear();
            _purchaser.CheckPurchasable();
        }
        
        private void Move()
        {
            _movingData.movable.transform.position = Input.mousePosition;
        }
    }
}