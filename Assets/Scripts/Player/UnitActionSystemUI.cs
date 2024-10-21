using System;
using UnityEngine;

namespace Player
{
    public class UnitActionSystemUI : MonoBehaviour
    {
        [SerializeField] private Transform actionButtonPrefab;
        [SerializeField] private Transform actionButtonsParent;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            CreateUnitActionButtons();
            UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
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
            }
        }

        private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs eventArgs)
        {
            CreateUnitActionButtons();
        }
    }
}
