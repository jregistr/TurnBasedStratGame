using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Grid
{
    public class LevelGrid : MonoBehaviour
    {
        
        public static LevelGrid Instance { get; private set; }
        
        [SerializeField] private Transform gridDebugPrefab;
        private GridSystem _gridSystem;

        public void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one instance of LevelGrid found!");
                Destroy(gameObject);
            }
            Instance = this;
            _gridSystem = new GridSystem(10, 10, 2);
            _gridSystem.CreateDebugObjects(gridDebugPrefab, transform);
        }

        public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
        {
            var gridObject = _gridSystem.GetGridObject(gridPosition);
            gridObject.AddUnit(unit);
        }

        public List<Unit> GetUnitsAtGridPosition(GridPosition gridPosition)
        {
            return _gridSystem.GetGridObject(gridPosition).GetOccupyingUnits();
        }

        public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
        {
            var gridObject = _gridSystem.GetGridObject(gridPosition);
            gridObject.RemoveUnit(unit);
        }

        public void UnitMoved(GridPosition oldPosition, GridPosition newPosition, Unit unit)
        {
            RemoveUnitAtGridPosition(oldPosition, unit);
            AddUnitAtGridPosition(newPosition, unit);
        }
        
        public GridPosition GetGridPosition(Vector3 worldPosition) => _gridSystem.GetGridPosition(worldPosition);
        
        public Vector3 GetWorldPosition(GridPosition gridPosition) => _gridSystem.GetWorldPosition(gridPosition);
        
        public bool IsValidGridPosition(GridPosition gridPosition) => _gridSystem.IsValidPosition(gridPosition);

        public bool IsGridPositionOccupied(GridPosition gridPosition)
        {
            var gridObject = _gridSystem.GetGridObject(gridPosition);
            return gridObject.IsOccupiedByAnyUnit();
        }
    }
}