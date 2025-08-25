using Pathfinding.Data;
using UnityEngine;
using Zenject;

namespace Pathfinding.Services
{
    [DefaultExecutionOrder(100)]
    // Provides mouse position in world space using raycast
    public class MouseWorldService
    {
        [Inject] private LevelGeneratorService _levelGeneratorService;

        // Shortcut to grid data
        public GridData Data => _levelGeneratorService.Data;

        // Returns the mouse position in world space by raycasting against the terrain
        public Vector3 GetPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, Data.TerrainLayer))
            {
                return raycastHit.point;
            }

            return Vector3.zero;
        }
    }
}
