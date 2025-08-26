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
        [Inject] private TimeScaleService _timeScaleService;

        [SerializeField] CanvasGroup _levelCompletedCV;
        [SerializeField] CanvasGroup _mainMenuCV;

        private void Awake()
        {
            _levelCompletedCV.gameObject.SetActive(false);
            _mainMenuCV.gameObject.SetActive(true);

            _timeScaleService.SetTimeToZero(); // Pause the game
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

            _timeScaleService.SetTimeToZero(); // Pause the game
        }

        // Quit game when level is complete
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
            _timeScaleService.SetTimeToNormal(); // Normal game speed
        }
    }
}