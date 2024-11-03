using System;
using Grid;
using Units.Actions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Unit = Units.Unit;

namespace Player
{
    public class UnitActionSystem : MonoBehaviour
    {
        
        public static UnitActionSystem Instance { get; private set; }

        public event EventHandler OnSelectedUnitChanged;
        public event EventHandler OnSelectedActionChanged;
        public event EventHandler<bool> OnUnitExecutingActionChanged;
        public event EventHandler UnitStartedAction;

        [SerializeField] private LayerMask mouseUnitLayerMask;
        public Unit SelectedUnit { get; private set; }
        public BaseAction SelectedAction { get; private set; }

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
            
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            
            if (_selectedUnitRunningAction)
            {
                return;
            }
            
            if (TryHandleUnitSelection())
            {
                return;
            }

            HandleSelectedAction();
        }

        private void HandleSelectedAction()
        {
            if (Input.GetMouseButtonUp(MouseButton.Left.GetHashCode()))
            {
                if (!SelectedUnit?.TrySpendActionPoints(SelectedAction) ?? false) return;
                UnitStartedAction?.Invoke(this, EventArgs.Empty);
                switch (SelectedAction)
                {
                    case MoveAction mouseMoveAction:
                        var position = MouseWorld.GetMouseWorldPosition();
                        var mouseGridPosition = LevelGrid.Instance.GetGridPosition(position);
                        if (SelectedUnit?.MoveAction.IsValidActionGridPosition(mouseGridPosition) ?? false)
                        {
                            TakingAction();
                            mouseMoveAction.MoveTo(mouseGridPosition, ClearTakingAction);
                        }
                        break;
                    case SpinAction spinAction:
                        TakingAction();
                        spinAction.Spin(ClearTakingAction);
                        break;
                }
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
                    if (unit == SelectedUnit)
                    {
                        return false;
                    }
                    SelectedUnit = unit;
                    SetSelectedAction(SelectedUnit.MoveAction);
                    OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
                    return true;
                }
            }

            return false;
        }

        private void TakingAction()
        {
            _selectedUnitRunningAction = true;
            OnUnitExecutingActionChanged?.Invoke(this, true);
        }

        private void ClearTakingAction()
        {
            _selectedUnitRunningAction = false;
            OnUnitExecutingActionChanged?.Invoke(this, false);
        }

        public void SetSelectedAction(BaseAction action)
        {
            SelectedAction = action;
            OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}