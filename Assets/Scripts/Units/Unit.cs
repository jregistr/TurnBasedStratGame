using System;
using Grid;
using Units.Actions;
using UnityEngine;

namespace Units
{
    public class Unit : MonoBehaviour
    {
        
        private GridPosition _gridPosition;
        public MoveAction MoveAction { get; private set; }

        private void Awake()
        {
            MoveAction = GetComponent<MoveAction>();
        }

        public void Start()
        {
            _gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            LevelGrid.Instance.AddUnitAtGridPosition(_gridPosition, this);
        }

        // Update is called once per frame
        private void Update()
        {
            var newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            if (newGridPosition != _gridPosition)
            {
                var oldGridPosition = _gridPosition;
                _gridPosition = newGridPosition;
                LevelGrid.Instance.UnitMoved(oldGridPosition, _gridPosition, this);
            }
        }
        
        public GridPosition GetGridPosition => _gridPosition;
    }
}
