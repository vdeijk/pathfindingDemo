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
            // Sets camera pan limits based on terrain mesh size
            _data.MaxPanX = _data.TerrainMesh.bounds.size.x;
            _data.MaxPanY = _data.TerrainMesh.bounds.size.z;
            _data.MaxPanY = _data.TerrainMesh.bounds.size.z;

            _overheadCameraService.Init(_data);
            _cameraCenteringMonobService.Init(_data);
        }
    }
}
