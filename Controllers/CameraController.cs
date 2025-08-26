using Pathfinding.Data;
using Pathfinding.Services;
using UnityEngine;
using Zenject;

namespace Pathfinding.Controllers
{
    [DefaultExecutionOrder(-100)]
    public class CameraController : MonoBehaviour
    {
        [Inject] private OverheadCameraService _overheadCameraService;
        [Inject] private CameraCenteringMonobService _cameraCenteringMonobService;

        [SerializeField] CameraData _data;

        private void Start()
        {
            // Get LevelPlane bounds
            Bounds planeBounds = _data.LevelPlane.GetComponent<MeshRenderer>().bounds;

            // Clamp position to plane bounds
            _data.MinPosX = planeBounds.min.x;
            _data.MaxPosX = planeBounds.max.x;
            _data.MinPosZ = planeBounds.min.z;
            _data.MaxPosZ = planeBounds.max.z;

            _overheadCameraService.Init(_data);
            _cameraCenteringMonobService.Init(_data);
        }
    }
}
