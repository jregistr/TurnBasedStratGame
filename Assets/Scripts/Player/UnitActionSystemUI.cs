using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Player
{
    public class UnitActionSystemUI : MonoBehaviour
    {
        [SerializeField] private Transform actionButtonPrefab;
        [SerializeField] private Transform actionButtonsParent;
        [SerializeField] private TextMeshProUGUI actionButtonText;

        private List<ActionButtonUI> _actionButtons;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            CreateUnitActionButtons();
            UpdateActionPointsVisual();
            UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
            UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
            UnitActionSystem.Instance.UnitStartedAction += UnitActionSystem_OnSelectedUnitStartedAction;
        }

        private void CreateUnitActionButtons()
        {
            _actionButtons = new List<ActionButtonUI>();
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

        private void UpdateActionPointsVisual()
        {
            var selectedUnit = UnitActionSystem.Instance.SelectedUnit;
            if (!selectedUnit)
            {
                actionButtonText.gameObject.SetActive(false);
                return;
            }
            
            actionButtonText.gameObject.SetActive(true);

            var points = selectedUnit.GetActionPointsLeft();
            actionButtonText.text = $"Action Points: {points}";
        }

        private void UnitActionSystem_OnSelectedUnitStartedAction(object sender, EventArgs eventArgs)
        {
            Debug.Log("Unit started action");
            UpdateActionPointsVisual();
        }

        private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs eventArgs)
        {
            CreateUnitActionButtons();
            UpdateActionPointsVisual();
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
