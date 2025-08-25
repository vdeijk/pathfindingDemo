using Pathfinding.Services;
using UnityEngine;
using Zenject;

namespace Pathfinding.Controllers
{
    [DefaultExecutionOrder(-100)]
    public class UIController : MonoBehaviour
    {
        [SerializeField] CanvasGroup _levelCompletedCV;
        [SerializeField] CanvasGroup _mainMenuCV;

        [Inject] private AgentMoveService _agentMoveService;
        [Inject] private GridController _gridController;

        private void Awake()
        {
            // Set initial UI state: show main menu, hide level completed screen
            _levelCompletedCV.gameObject.SetActive(false);
            _mainMenuCV.gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            // Listen for level completion events
            LevelProgressionService.OnLevelCompleted += LevelProgressionService_OnLevelCompleted;
        }

        private void OnDisable()
        {
            LevelProgressionService.OnLevelCompleted -= LevelProgressionService_OnLevelCompleted;
        }

        // Show level completed UI when level is finished
        private void LevelProgressionService_OnLevelCompleted(object sender, System.EventArgs e)
        {
            _levelCompletedCV.gameObject.SetActive(true);
        }

        // Called by UI button to start a new level
        public void CompleteLevel()
        {
            _levelCompletedCV.gameObject.SetActive(false);
            _gridController.CreateNewLevel();
            _agentMoveService.TeleportPlayerToEntrance();
        }

        // Called by UI button to start the game
        public void StartLevel()
        {
            _mainMenuCV.gameObject.SetActive(false);
        }
    }
}