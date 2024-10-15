using System;
using UnityEngine;
using Cinemachine;

namespace Player
{
    public class CameraController : MonoBehaviour
    {
        private const float MinFollowYOffset = 2f;
        private const float MaxFollowYOffset = 12f;
        
        [SerializeField] private float cameraSpeed = 10f;
        [SerializeField] private float rotationSpeed = 100f;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private float zoomSpeed = 10f;
        
        private CinemachineTransposer _transposer;

        private Vector3 _targetFollowOffset;

        private void Awake()
        {
            _transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        }

        private void Start()
        {
            _targetFollowOffset = _transposer.m_FollowOffset;
        }

        // Update is called once per frame
        private void Update()
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

            if (Input.mouseScrollDelta.y != 0)
            {
                _targetFollowOffset.y += Input.mouseScrollDelta.y;
            }
            
            _targetFollowOffset.y = Mathf.Clamp(_targetFollowOffset.y, MinFollowYOffset, MaxFollowYOffset);
            
            _transposer.m_FollowOffset = Vector3.Lerp(_transposer.m_FollowOffset, _targetFollowOffset, Time.deltaTime * zoomSpeed);
        }
    }
}
