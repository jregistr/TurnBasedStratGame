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
        }
        
        private void Start()
        {
            _gridSystem = new GridSystem(10, 10, 2);
            _gridSystem.CreateDebugObjects(gridDebugPrefab, transform);
        }

        public void SetUnitAtGridPosition(GridPosition gridPosition, Unit unit)
        {
            var gridObject = _gridSystem.GetGridObject(gridPosition);
            gridObject.SetUnit(unit);
        }

        public Unit GetUnitAtGridPosition(GridPosition gridPosition)
        {
            return _gridSystem.GetGridObject(gridPosition).OccupyingUnit;
        }

        public void ClearUnitAtGridPosition(GridPosition gridPosition)
        {
            // var gridObject = _gridSystem.GetGridObject(gridPosition);
            // gridObject.SetUnit(null);
            SetUnitAtGridPosition(gridPosition, null);
        }

        public void UnitMoved(GridPosition oldPosition, GridPosition newPosition, Unit unit)
        {
            ClearUnitAtGridPosition(oldPosition);
            SetUnitAtGridPosition(newPosition, unit);
        }
        
        public GridPosition GetGridPosition(Vector3 worldPosition) => _gridSystem.GetGridPosition(worldPosition);
    }
}