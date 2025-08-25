using Pathfinding.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Pathfinding.Services
{
    [DefaultExecutionOrder(200)]
    public class InputService
    {
        public GameData Data;

        [Inject] private MouseWorldService _mouseWorldService;
        [Inject] private LevelGeneratorService _levelGeneratorService;
        [Inject] private AgentCategoryService _agentCategoryService;
        [Inject] private OverheadCameraService _overheadCameraService;
        [Inject] private LevelUtilityService _levelUtilityService;
        [Inject] private AgentMoveService _agentMoveService;

        // Initializes the service with Inspector-assigned game data
        public void Init(GameData data)
        {
            Data = data;
        }

        // Handles grid visibility toggling
        public void HandleGridInput()
        {
            _levelGeneratorService.ToggleGrid(Data.ShowGridInput);
        }

        // Handles pause input (expand for pause logic)
        public void HandlePauseInput()
        {
            if (!Data.PauseInput) return;
        }

        // Handles camera movement, rotation, and zoom
        public void HandleCameraInput()
        {
            _overheadCameraService.UpdatePosition(Data.MoveInputs);
            _overheadCameraService.UpdateRotation(Data.RotateInput);
            _overheadCameraService.UpdateZoom(Data.ZoomInput);
        }

        // Handles mouse input for agent movement
        public void HandleMouseInput()
        {
            bool isOverUI = EventSystem.current.IsPointerOverGameObject();
            if (Data.LeftMouseInput && !isOverUI)
            {
                AgentData data = _agentCategoryService.Data.Player;
                Vector3 mousePos = _mouseWorldService.GetPosition();
                Vector2Int mouseGridPosition = _levelUtilityService.GetGridPosition(mousePos);
                data.MovementData.TargetPos = mouseGridPosition;

                _agentMoveService.StartAction(data);
            }
        }

        // Obtains user input and updates game data
        public void ObtainUserInputs()
        {
            Data.MoveInputs = ObtainCameraInputsMove();
            Data.ZoomInput = Input.mouseScrollDelta.y;
            Data.PauseInput = Input.GetKeyDown(KeyCode.Escape);
            Data.LeftMouseInput = Input.GetMouseButtonDown(0);
            Data.RightMouseInput = Input.GetMouseButtonDown(1);
            Data.ShowGridInput = Input.GetKeyDown(KeyCode.G);

            float rotateInput = 0f;
            if (Input.GetKey(KeyCode.Q)) rotateInput -= 1f;
            if (Input.GetKey(KeyCode.E)) rotateInput += 1f;
            Data.RotateInput = rotateInput;
        }

        // Returns camera movement input vector based on WASD keys
        private Vector3 ObtainCameraInputsMove()
        {
            Vector3 inputMoveDir = new Vector3(0, 0, 0);

            if (Input.GetKey(KeyCode.W))
            {
                inputMoveDir.z = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                inputMoveDir.z = -1;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                inputMoveDir.x = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                inputMoveDir.x = 1;
            }

            return inputMoveDir;
        }
    }
}