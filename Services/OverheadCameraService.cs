using Pathfinding.Data;
using UnityEngine;
using Zenject;

namespace Pathfinding.Services
{
    [DefaultExecutionOrder(100)]
    public class OverheadCameraService
    {
        [Inject] private AgentCategoryService _agentCategoryService;

        public CameraData Data { get; private set; }

        private float _targetZoom;
        private float _curZoom => Data.CinemachineThirdPersonFollow.VerticalArmLength;

        // Initializes camera data and sets default zoom
        public void Init(CameraData data)
        {
            Data = data;
            Data.DefaultZoom = (Data.MaxZoom + Data.MinZoom) / 2;
            _targetZoom = Data.DefaultZoom;
            Data.CinemachineThirdPersonFollow.VerticalArmLength = Data.DefaultZoom;
            Data.CinemachineThirdPersonFollow.CameraDistance = Data.DefaultZoom;
        }

        // Sets camera position and rotation to follow the player
        public void InitPosition()
        {
            Data.TrackingTargetTransform.rotation = Quaternion.identity;
            Data.TrackingTargetTransform.position = _agentCategoryService.Data.Player.MovementData.BodyTransform.position;
        }

        // Updates camera position based on input
        public void UpdatePosition(Vector3 cameraMoveInputs)
        {
            if (cameraMoveInputs == Vector3.zero) return;

            float moveSpeed = Mathf.Sqrt((Data.MoveSpeed * _targetZoom) / Data.DefaultZoom);
            Vector3 moveVector = Data.TrackingTargetTransform.forward * cameraMoveInputs.z + Data.TrackingTargetTransform.right * cameraMoveInputs.x;
            Vector3 newPos = Data.TrackingTargetTransform.position + moveVector * moveSpeed * Time.unscaledDeltaTime;

            float clampedX = Mathf.Clamp(newPos.x, Data.MinPosX, Data.MaxPosX);
            float clampedZ = Mathf.Clamp(newPos.z, Data.MinPosZ, Data.MaxPosZ);

            Data.TrackingTargetTransform.position = new Vector3(clampedX, Data.TrackingTargetTransform.position.y, clampedZ);
        }

        // Updates camera zoom based on input
        public void UpdateZoom(float input)
        {
            if (input != 0)
            {
                float zoomChange = input * Time.unscaledDeltaTime * Data.ZoomSpeed;
                _targetZoom = Mathf.Clamp(Data.CinemachineThirdPersonFollow.CameraDistance + zoomChange, Data.MinZoom, Data.MaxZoom);

                Data.CinemachineThirdPersonFollow.VerticalArmLength = Mathf.Lerp(_curZoom, _targetZoom, Time.unscaledDeltaTime * Data.ZoomSpeed);
                Data.CinemachineThirdPersonFollow.CameraDistance = Mathf.Lerp(_curZoom, _targetZoom, Time.unscaledDeltaTime * Data.ZoomSpeed);
            }
        }

        // Updates camera rotation based on input
        public void UpdateRotation(float cameraRotateInput)
        {
            if (cameraRotateInput != 0)
            {
                float rotateChange = cameraRotateInput * Time.unscaledDeltaTime * Data.RotateSpeed;
                Data.TrackingTargetTransform.Rotate(0, rotateChange, 0);
            }
        }
    }
}
