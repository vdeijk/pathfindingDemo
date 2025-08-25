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
        [Inject] private OverheadCameraService _overheadCameraService;

        private void Awake()
        {
            // Set initial UI state: show main menu, hide level completed screen
            _levelCompletedCV.gameObject.SetActive(false);
            _mainMenuCV.gameObject.SetActive(true);

            Time.timeScale = 0f; // Pause the game at start
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
            Time.timeScale = 0f;
        }

        // Called by UI button to start a new level
        public void CompleteLevel()
        {
            _levelCompletedCV.gameObject.SetActive(false);
            _gridController.CreateNewLevel();
            _agentMoveService.TeleportPlayerToEntrance();
            _overheadCameraService.InitPosition();
            Time.timeScale = 1f;
        }

        // Called by UI button to start the game
        public void StartLevel()
        {
            _mainMenuCV.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}