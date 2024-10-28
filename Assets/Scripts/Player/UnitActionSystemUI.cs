using System;
using System.Collections.Generic;
using Units.Actions;
using UnityEngine;

namespace Player
{
    public class UnitActionSystemUI : MonoBehaviour
    {
        [SerializeField] private Transform actionButtonPrefab;
        [SerializeField] private Transform actionButtonsParent;

        private List<ActionButtonUI> _actionButtons;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            CreateUnitActionButtons();
            UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
            UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
            _actionButtons = new List<ActionButtonUI>();
        }

        private void CreateUnitActionButtons()
        {
            foreach (Transform actionButton in actionButtonsParent)
            {
                Destroy(actionButton.gameObject);
            }
            
            var selectedUnit = UnitActionSystem.Instance.SelectedUnit;
            if (!selectedUnit) return;
            
            foreach (var baseAction in selectedUnit.BaseActions)
            {
                var button = Instantiate(actionButtonPrefab, actionButtonsParent);
                var actionButtonUi = button.GetComponent<ActionButtonUI>();
                actionButtonUi.SetBaseAction(baseAction);
                _actionButtons.Add(actionButtonUi);
            }

            UpdateSelectedActionVisual();
        }

        private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs eventArgs)
        {
            CreateUnitActionButtons();
        }

        private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs eventArgs)
        {
            UpdateSelectedActionVisual();
        }

        private void UpdateSelectedActionVisual()
        {
            var selectedActionButton = UnitActionSystem.Instance.SelectedAction;
            foreach (var actionButtonUI in _actionButtons)
            {
                actionButtonUI.ShowSelected(actionButtonUI.IsForAction(selectedActionButton));
            }
        }
    }
}
