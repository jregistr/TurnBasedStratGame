using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Units.Actions
{
    public class MoveAction : BaseAction
    {
        private static readonly int Moving = Animator.StringToHash("Moving");

        [SerializeField] private int maxMoveDistance = 3;

        [SerializeField] private Animator unitAnimator;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private float stoppingDistance;

        public bool IsMoving {get; private set;}
        
        private Vector3 _targetPosition;

        protected override void Awake()
        {
            base.Awake();
            _targetPosition = transform.position;
        }

        private void Update()
        {
            if (!_isActive) return;
            
            var distanceToTarget = Vector3.Distance(transform.position, _targetPosition);
            if (distanceToTarget > stoppingDistance)
            {
                var moveDirection = (_targetPosition - transform.position).normalized;
                transform.position += moveDirection * (moveSpeed * Time.deltaTime);
                transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);
                IsMoving = true;
                unitAnimator.SetBool(Moving, true);
            }
            else
            {
                IsMoving = false;
                unitAnimator.SetBool(Moving, false);
                _isActive = false;
            }
        }

        public void MoveTo(GridPosition targetPosition)
        {
            _isActive = true;
            _targetPosition = LevelGrid.Instance.GetWorldPosition(targetPosition);
        }

        public bool IsValidActionGridPosition(GridPosition targetPosition)
        {
            List<GridPosition> validPosition = GetValidActionGridPositionList();
            return validPosition.Contains(targetPosition);
        }
        
        public List<GridPosition> GetValidActionGridPositionList()
        {
            var list = new List<GridPosition>();
            GridPosition currentGridPosition = _unit.GetGridPosition;
            for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
            {
                for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
                {
                    GridPosition offsetPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = currentGridPosition + offsetPosition;
                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                    {
                        continue;
                    }

                    if (currentGridPosition == testGridPosition)
                    {
                        continue;
                    }

                    if (LevelGrid.Instance.IsGridPositionOccupied(testGridPosition))
                    {
                        continue;
                    }

                    list.Add(testGridPosition);
                }
            }

            return list;
        }
    }
}