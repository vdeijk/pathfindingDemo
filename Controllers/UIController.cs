using Pathfinding.Services;
using UnityEngine;
using Zenject;
using Pathfinding.Data;

namespace Pathfinding.Controllers
{
    [DefaultExecutionOrder(-100)]
    public class UIController : MonoBehaviour
    {
        [Inject] private AgentSpawnService _agentSpawnService;
        [Inject] private MenuFadeMonobService _menuFadeMonobService;
        [Inject] private TimeScaleService _timeScaleService;

        [SerializeField] UIData _data;

        private void Awake()
        {
            _data.LevelCompletedCV.gameObject.SetActive(false);
            _data.MainMenuCV.gameObject.SetActive(true);

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
            _menuFadeMonobService.Fade(true, _data.LevelCompletedCV);

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

            _menuFadeMonobService.Fade(false, _data.MainMenuCV);
            _timeScaleService.SetTimeToNormal(); // Normal game speed
        }
    }
}