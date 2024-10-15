using Grid;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private static readonly int Moving = Animator.StringToHash("Moving");
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float stoppingDistance;
    
    [SerializeField] private Animator unitAnimator;
    
    private Vector3 _targetPosition;
    private GridPosition _gridPosition;

    private void Awake()
    {
        _targetPosition = transform.position;
    }

    public void Start()
    {
        _gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(_gridPosition, this);
    }

    // Update is called once per frame
    private void Update()
    {
        var distanceToTarget = Vector2.Distance(transform.position, _targetPosition);
        if (distanceToTarget > stoppingDistance)
        {
            var moveDirection = (_targetPosition - transform.position).normalized;
            
            transform.position += moveDirection * (moveSpeed * Time.deltaTime);
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);
            unitAnimator.SetBool(Moving, true);
            
            var newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            if (newGridPosition != _gridPosition)
            {
                var oldGridPosition = _gridPosition;
                _gridPosition = newGridPosition;
                LevelGrid.Instance.UnitMoved(oldGridPosition, _gridPosition, this);
            }
        }
        else
        {
            unitAnimator.SetBool(Moving, false);
        }
    }

    public void Move(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }
}
