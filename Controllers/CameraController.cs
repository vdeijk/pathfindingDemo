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

        [SerializeField] CameraData Data;

        private void Start()
        {
            Init();
            // Initialize camera service with camera data
            _overheadCameraService.Init(Data);
            _cameraCenteringMonobService.Init(Data);

        }

        // Sets camera pan limits based on terrain mesh size
        private void Init()
        {
            Data.MaxPanX = Data.TerrainMesh.bounds.size.x;
            Data.MaxPanY = Data.TerrainMesh.bounds.size.z;
            Data.MaxPanY = Data.TerrainMesh.bounds.size.z;
        }
    }
}
