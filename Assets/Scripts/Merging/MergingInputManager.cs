using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Merging
{
    public partial class MergingInputManager : MonoBehaviour
    {
        [SerializeField] private MergingPurchaser _purchaser;
        [SerializeField] private MergingLogic _mergingLogic;
        [SerializeField] private GraphicRaycaster _raycaster;
        [SerializeField] private EventSystem _eventSystem;
        [Space(20)]
        private PointerEventData _pointerEventData;
        public Image movingImage;
        private MovingData _movingData;
        
        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set => _isEnabled = value;
        }

        private void Awake()
        {
            _movingData = new MovingData();
            _movingData.movable = movingImage.transform;
            _movingData.image = movingImage;
            movingImage.enabled = false;
            IsEnabled = true;
        }
        
        private void Update()
        {
            if (_isEnabled == false)
                return;
            if (Input.GetMouseButtonDown(0))
            {
                var results = Raycast();
                foreach (var result in results)
                {
                    var area = result.gameObject.GetComponent<MergingItemArea>();
                    if (area != null)
                    {
                        PickFrom(area);
                        return;
                    }
                }
            }   
            else if (Input.GetMouseButtonUp(0))
            {
                var results = Raycast();
                if (results.Count == 0)
                {
                    DropAt(null);
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
            
            if (Input.GetMouseButton(0))
            {
                Move(Vector3.zero);
            }
        }

        private List<RaycastResult> Raycast()
        {
            _pointerEventData = new PointerEventData(_eventSystem);
            _pointerEventData.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            _raycaster.Raycast(_pointerEventData, results);
            return results;
        }

        private void PickFrom(MergingItemArea area)
        {
            var otherData = area.currentData;
            if (otherData == null)
            {
                _purchaser.TryPurchase(area);
                return;
            }
            _movingData.fromArea = area;
            _movingData.Show(otherData);
            _movingData.movable.transform.position = Input.mousePosition;
            area.TakeFrom();
        }

        private void DropAt(MergingItemArea area)
        {
            if (_movingData.data == null)
            {
                _purchaser.CheckPurchase();
                return;
            }
            if (area == null)
            {
                _movingData.fromArea.SetData(_movingData.data);
            }
            else
            {
                // Empty or the same we took it from (should be empty)
                if (area.IsEmpty())
                    area.SetData(_movingData.data);
                else
                {
                    var merged = _mergingLogic.Merge(_movingData.data, area);
                    if (merged)
                    {
                        // Debug.Log("merged success");   
                    }
                    else
                    {
                        _movingData.fromArea.SetData(_movingData.data);
                    }
                }
            }
            _movingData.HideAndClear();
            _purchaser.CheckPurchase();
        }
        
        private void Move(Vector2 moveDir)
        {
            _movingData.movable.transform.position = Input.mousePosition;
        }
    }
}