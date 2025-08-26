using Pathfinding.Data;
using Pathfinding.Services;
using UnityEngine;
using Zenject;

namespace Pathfinding.Controllers
{
    [DefaultExecutionOrder(-110)]
    public class LevelController : MonoBehaviour
    {
        [Inject] private LevelGeneratorService _levelGeneratorService;
        [Inject] private LevelUtilityService _levelUtilityService;
        [Inject] private LevelSpawnService _propSpawnService;

        [SerializeField] LevelData _gridData;
        [SerializeField] LevelSpawnData _vegetationSpawnData;
        [SerializeField] LevelSpawnData _propSpawnData;

        private void Start()
        {
            // Assign spawn data and initialize services
            _levelUtilityService.Init(_gridData);
            _levelGeneratorService.Init(_gridData, _vegetationSpawnData, _propSpawnData);

            // Optionally create a new level at startup
            if (_gridData.CreateLevelOnPlay)
            {
                CreateLevel();
            }
        }

        // Creates a new level and places grid squares and props
        public void CreateLevel()
        {
            _gridData.ValidGridPositions.Clear();
            DestroySquares();
            _levelGeneratorService.CreateGrid();
            _propSpawnService.SetNoiseMap(_propSpawnData);
            _propSpawnService.SetNoiseMap(_vegetationSpawnData);
            _levelGeneratorService.SetEntrancAndExit();
            _levelGeneratorService.PlaceSquares();
        }

        // Destroys all grid square GameObjects under the parent
        public void DestroySquares()
        {
            for (int i = _gridData.Parent.childCount; i > 0; --i)
            {
                DestroySquare();
            }
        }

        // Destroys the first child grid square GameObject
        public void DestroySquare()
        {
            DestroyImmediate(_gridData.Parent.GetChild(0).gameObject);
        }
    }
}