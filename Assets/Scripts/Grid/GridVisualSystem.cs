using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Grid
{
    public class GridVisualSystem : MonoBehaviour
    {
        
        public static GridVisualSystem Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            Instance = this;
        }
        
        [SerializeField] private Transform visualPrefab;

        private GridVisualItem[,] _gridVisuals;
        
        private void Start()
        {
            _gridVisuals = new GridVisualItem[
                LevelGrid.Instance.GetGridWidth(),
                LevelGrid.Instance.GetGridHeight()
            ];
            
            for (var x = 0; x < LevelGrid.Instance.GetGridWidth(); x++)
            {
                for (var z = 0; z < LevelGrid.Instance.GetGridHeight(); z++)
                {
                    var gridPosition = new GridPosition(x, z);
                    var worldPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
                    var visual = Instantiate(visualPrefab, worldPosition, Quaternion.identity);
                    visual.name = $"{x}_{z}";
                    visual.transform.SetParent(transform);
                    var gridVisualItem = visual.GetComponent<GridVisualItem>();
                    _gridVisuals[x, z] = gridVisualItem;
                    gridVisualItem.Hide();
                }
            }
        }

        private void Update()
        {
            UpdateGridVisuals();
        }

        public void HideAllGridVisuals()
        {
            foreach (var visual in _gridVisuals)
            {
               visual.Hide();
            }
        }

        public void ShowPositions(List<GridPosition> positions)
        {
            foreach (var position in positions)
            {
                _gridVisuals[position.X, position.Z].Show();
            }
        }

        public void UpdateGridVisuals()
        {
            HideAllGridVisuals();
            var selectedUnit = UnitActionSystem.Instance.SelectedUnit;
            if (!selectedUnit) return;
            if (selectedUnit.MoveAction.IsMoving) return;
            var validMovePositions = selectedUnit.MoveAction.GetValidActionGridPositionList();
            ShowPositions(validMovePositions);
        }
    }
}