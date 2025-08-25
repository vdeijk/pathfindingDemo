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

        [SerializeField] CameraData Data;

        private void Start()
        {
            Init();
            _overheadCameraService.Init(Data);
        }

        private void Init()
        {
            Data.MaxPanX = Data.TerrainMesh.bounds.size.x;
            Data.MaxPanY = Data.TerrainMesh.bounds.size.z;
            Data.MaxPanY = Data.TerrainMesh.bounds.size.z;
        }
    }
}
