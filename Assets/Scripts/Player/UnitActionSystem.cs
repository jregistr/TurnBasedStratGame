using System;
using Grid;
using Unity.VisualScripting;
using UnityEngine;
using Unit = Units.Unit;

namespace Player
{
    public class UnitActionSystem : MonoBehaviour
    {
        public static UnitActionSystem Instance { get; private set; }

        public event EventHandler OnSelectedUnitChanged;

        [SerializeField] private LayerMask mouseUnitLayerMask;
        public Unit SelectedUnit { get; private set; }

        private bool _selectedUnitRunningAction;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError($"There is more than one unit action system: ${transform} - ${Instance}");
                Destroy(gameObject);
            }

            Instance = this;
        }
        
        // Update is called once per frame
        private void Update()
        {
            if (TryHandleUnitSelection())
            {
                return;
            }

            if (_selectedUnitRunningAction)
            {
                return;
            }

            if (Input.GetMouseButtonUp(MouseButton.Left.GetHashCode()))
            {
                
                var position = MouseWorld.GetMouseWorldPosition();
                GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(position);
                if (SelectedUnit?.MoveAction.IsValidActionGridPosition(mouseGridPosition) ?? false)
                {
                    TakingAction();
                    Debug.Log($"Valid action grid position: {mouseGridPosition}");
                    SelectedUnit.MoveAction.MoveTo(mouseGridPosition, ClearTakingAction);
                }
            }

            if (Input.GetMouseButtonDown(MouseButton.Right.GetHashCode()))
            {
                TakingAction();
                SelectedUnit?.SpinAction.Spin(ClearTakingAction);
            }
        }

        private bool TryHandleUnitSelection()
        {
            if (Input.GetMouseButtonUp(MouseButton.Left.GetHashCode()))
            {
                var ray = Camera.main!.ScreenPointToRay(Input.mousePosition);
                var raycastHit = Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, mouseUnitLayerMask);

                if (raycastHit && hitInfo.collider.TryGetComponent(out Unit unit))
                {
                    SelectedUnit = unit;
                    OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
                    return true;
                }
            }

            return false;
        }

        private void TakingAction()
        {
            _selectedUnitRunningAction = true;
        }

        private void ClearTakingAction()
        {
            _selectedUnitRunningAction = false;
        }
    }
}