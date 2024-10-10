using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float stoppingDistance;
    
    private Vector3 _targetPosition;
    
    // Update is called once per frame
    private void Update()
    {
        var distanceToTarget = Vector2.Distance(transform.position, _targetPosition);
        if (distanceToTarget > stoppingDistance)
        {
            var moveDirection = (_targetPosition - transform.position).normalized;
            transform.position += moveDirection * (moveSpeed * Time.deltaTime);
        }

        if (Input.GetMouseButtonUp(MouseButton.Left.GetHashCode()))
        {
            var position = MouseWorld.GetMouseWorldPosition();
            Move(position);
        }
    }

    private void Move(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }
}
