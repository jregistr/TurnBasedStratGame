using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private static readonly int Moving = Animator.StringToHash("Moving");
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float stoppingDistance;
    
    [SerializeField] private Animator unitAnimator;
    
    private Vector3 _targetPosition;
    
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
        }
        else
        {
            unitAnimator.SetBool(Moving, false);
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
