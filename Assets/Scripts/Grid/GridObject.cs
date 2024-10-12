using System;

namespace Grid
{
    public class GridObject
    {
        private readonly GridSystem _gridSystem;
        public readonly GridPosition GridPosition;
        
        public Unit OccupyingUnit {get; private set;}

        public GridObject(GridSystem gridSystem, GridPosition gridPosition)
        {
            _gridSystem = gridSystem;
            GridPosition = gridPosition;
            OccupyingUnit = null;
        }

        public void SetUnit(Unit unit)
        {
            if (OccupyingUnit != null && unit == null)
            {
                OccupyingUnit = null;
                return;
            }
            
            if (OccupyingUnit != null)
            {
                throw new ArgumentException("Unit is already occupied");
            }
            
            OccupyingUnit = unit;
        }

        public override string ToString()
        {
            return $"{GridPosition.X},{GridPosition.Z}\n" +
                   $"{(OccupyingUnit != null ? OccupyingUnit.name : string.Empty)}";
        }
    }
}