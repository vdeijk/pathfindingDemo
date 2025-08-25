using Pathfinding.Data;
using Pathfinding.Services;
using UnityEngine;
using Zenject;

namespace Pathfinding.Controllers
{
    [DefaultExecutionOrder(-100)]
    public class AgentsController : MonoBehaviour
    {
        [Inject] private AgentCategoryService _agentCategoryService;
        [Inject] private AgentSpawnService _agentSpawnService;

        [SerializeField] AgentsData Data;

        private void Start()
        {
            // Initialize agent category and spawn services with Inspector-assigned data
            _agentCategoryService.Init(Data);
            _agentSpawnService.Init(Data);
        }
    }
}