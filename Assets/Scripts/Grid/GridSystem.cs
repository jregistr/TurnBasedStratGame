using UnityEngine;

namespace Grid
{
    public class GridSystem
    {
        private readonly int _width;
        private readonly int _height;
        private readonly float _cellSize;
        private readonly GridObject[,] _grid;

        public GridSystem(int width, int height, float cellSize)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _grid = new GridObject[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    GridPosition position = new GridPosition(x, z);
                    _grid[x, z] = new GridObject(this, position);
                }
            }
        }

        public Vector3 GetWorldPosition(GridPosition position)
        {
            return new Vector3(position.X, 0, position.Z) * _cellSize;
        }

        public GridPosition GetGridPosition(Vector3 worldPosition)
        {
            return new GridPosition(
                Mathf.RoundToInt(worldPosition.x / _cellSize),
                Mathf.RoundToInt(worldPosition.z / _cellSize)
            );
        }

        public void CreateDebugObjects(Transform debugPrefab, Transform parent)
        {
            for (int x = 0; x < _width; x++)
            {
                for (int z = 0; z < _height; z++)
                {
                    var position = new GridPosition(x, z);
                    var debug = Object.Instantiate(debugPrefab,
                        GetWorldPosition(position),
                        Quaternion.identity
                    );
                    debug.name = $"GridObject_{x}_{z}";
                    debug.SetParent(parent, false);
                    var gridDebugObject = debug.GetComponent<GridDebugObject>()!;
                    gridDebugObject.Initialize(GetGridObject(position));
                }
            }
        }

        public GridObject GetGridObject(GridPosition gridPosition)
        {
            return _grid[gridPosition.X, gridPosition.Z];
        }

        public bool IsValidPosition(GridPosition gridPosition)
        {
            return gridPosition.X >= 0 && gridPosition.X < _width && 
                   gridPosition.Z >= 0 && gridPosition.Z < _height;
        }
    }
}