using System;
using Player;
using UnityEngine;

namespace Units
{
    public class UnitSelectedVisual : MonoBehaviour
    {
        private Unit _unit;
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _unit = GetComponentInParent<Unit>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.enabled = false;
        }

        private void Start()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnUnitSelected;
        }

        private void OnDestroy()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged -= UnitActionSystem_OnUnitSelected;
        }

        private void UnitActionSystem_OnUnitSelected(object sender, EventArgs e)
        {
            var selectedUnit = UnitActionSystem.Instance.SelectedUnit;
            if (selectedUnit == _unit)
            {
                _meshRenderer.enabled = true;
            }
            else
            {
                _meshRenderer.enabled = false;
            }
        }
    }
}