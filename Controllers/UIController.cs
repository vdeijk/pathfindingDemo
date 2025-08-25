using Pathfinding.Services;
using UnityEngine;
using Zenject;

namespace Pathfinding.Controllers
{
    [DefaultExecutionOrder(-100)]
    public class UIController : MonoBehaviour
    {
        [Inject] private AgentSpawnService _agentSpawnService;
        [Inject] private MenuFadeMonobService _menuFadeMonobService;

        [SerializeField] CanvasGroup _levelCompletedCV;
        [SerializeField] CanvasGroup _mainMenuCV;


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
            _menuFadeMonobService.Fade(true, _levelCompletedCV);

            Time.timeScale = 0f;
        }

        // Called by UI button to start a new level
        public void CompleteLevel()
        {
            Application.Quit();
        }

        // Called by UI button to start the game
        public void StartLevel()
        {
            _agentSpawnService.SpawnPlayer();
            _agentSpawnService.SpawnEnemies();

            _menuFadeMonobService.Fade(false, _mainMenuCV);

            Time.timeScale = 1f;
        }
    }
}