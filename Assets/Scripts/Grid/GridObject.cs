using System;
using System.Collections.Generic;
using System.Linq;

namespace Grid
{
    public class GridObject
    {
        private readonly GridSystem _gridSystem;
        public readonly GridPosition GridPosition;
        
        // public Unit OccupyingUnit {get; private set;}
        private List<Unit> _units;

        public GridObject(GridSystem gridSystem, GridPosition gridPosition)
        {
            _gridSystem = gridSystem;
            GridPosition = gridPosition;
            _units = new List<Unit>();
        }

        public void AddUnit(Unit unit)
        {
            if (_units.Contains(unit))
            {
                throw new ArgumentException("Unit is already in this grid position");
            }
            
            _units.Add(unit);
        }

        public void RemoveUnit(Unit unit)
        {
            _units.Remove(unit);
        }

        public List<Unit> GetOccupyingUnits()
        {
            return _units.AsReadOnly().ToList();
        }

        public override string ToString()
        {
            var unitNames = _units.Select(u => u.name);
            var unitNamesString = string.Join(",\n", unitNames);
            return $"{GridPosition.X},{GridPosition.Z}\n" +
                   $"{unitNamesString}";
        }
    }
}