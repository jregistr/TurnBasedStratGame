using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Serialization;

namespace Player
{
    public class CameraController : MonoBehaviour
    {
        private const float MinFollowYOffset = 2f;
        private const float MaxFollowYOffset = 12f;
        
        [SerializeField] private float cameraSpeed = 10f;
        [SerializeField] private float rotationSpeed = 100f;
        [SerializeField] private CinemachineCamera cinemachineCamera;
        [SerializeField] private float zoomSpeed = 10f;
        
        private CinemachineFollow _cinemachineFollow;

        private Vector3 _targetFollowOffset;

        private void Awake()
        {
            _cinemachineFollow = cinemachineCamera.GetComponent<CinemachineFollow>();
        }

        private void Start()
        {
            _targetFollowOffset = _cinemachineFollow.FollowOffset;
        }

        // Update is called once per frame
        private void Update()
        {
            HandleCameraMovements();

            HandleCameraRotation();

            HandleCameraZoom();
        }

        private void HandleCameraZoom()
        {
            if (Input.mouseScrollDelta.y != 0)
            {
                _targetFollowOffset.y += Input.mouseScrollDelta.y;
            }
            
            _targetFollowOffset.y = Mathf.Clamp(_targetFollowOffset.y, MinFollowYOffset, MaxFollowYOffset);
            
            _cinemachineFollow.FollowOffset = Vector3.Lerp(_cinemachineFollow.FollowOffset, _targetFollowOffset, Time.deltaTime * zoomSpeed);
        }

        private void HandleCameraRotation()
        {
            var rotationVector = Vector3.zero;
            if (Input.GetKey(KeyCode.E))
            {
                rotationVector += Vector3.down;
            }

            if (Input.GetKey(KeyCode.Q))
            {
                rotationVector += Vector3.up;
            }
            
            transform.eulerAngles += rotationVector * (rotationSpeed * Time.deltaTime);
        }

        private void HandleCameraMovements()
        {
            Vector3 inputMoveDirection = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.W))
            {
                inputMoveDirection += Vector3.forward;
            }

            if (Input.GetKey(KeyCode.S))
            {
                inputMoveDirection += Vector3.back;
            }

            if (Input.GetKey(KeyCode.A))
            {
                inputMoveDirection += Vector3.left;
            }

            if (Input.GetKey(KeyCode.D))
            {
                inputMoveDirection += Vector3.right;
            }
            
            var moveVector = transform.forward * inputMoveDirection.z + transform.right * inputMoveDirection.x;
            transform.position += moveVector * (cameraSpeed * Time.deltaTime);
        }
    }
}
