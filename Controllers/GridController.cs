using Pathfinding.Data;
using Pathfinding.Services;
using UnityEngine;
using Zenject;

namespace Pathfinding.Controllers
{
    [DefaultExecutionOrder(-110)]
    public class GridController : MonoBehaviour
    {
        [SerializeField] GridData Data;
        [SerializeField] GridSpawnData VegetationSpawnData;
        [SerializeField] GridSpawnData PropSpawnData;

        [Inject] private LevelGeneratorService _levelGeneratorService;
        [Inject] private LevelUtilityService _levelUtilityService;
        [Inject] private PropSpawnService _propSpawnService;

        private void Start()
        {
            Data.PropSpawnData = PropSpawnData;
            Data.VegetationSpawnData = VegetationSpawnData;
            _levelUtilityService.Init(Data);


            _levelGeneratorService.Init(Data);


            if (Data.CreateLevelOnPlay)
            {
                CreateNewLevel();
            }
        }

        public void CreateNewLevel()
        {
            Data.ValidGridPositions.Clear();
            _levelGeneratorService.CreateGrid();
            _propSpawnService.SetNoiseMap(Data.PropSpawnData);
            _propSpawnService.SetNoiseMap(Data.VegetationSpawnData);
            DestroyGridSquares();
            _levelGeneratorService.PlaceGridSquares();
            _levelGeneratorService.SetEntrancAndExit();
        }


        public void DestroyGridSquares()
        {
            for (int i = Data.Parent.childCount; i > 0; --i)
            {
                DestroyGridSquare();
            }
        }

        public void DestroyGridSquare()
        {
            DestroyImmediate(Data.Parent.GetChild(0).gameObject);
        }
    }
}