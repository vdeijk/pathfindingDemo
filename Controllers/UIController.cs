using Pathfinding.Services;
using System.Drawing.Text;
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
            _levelCompletedCV.gameObject.SetActive(false);
            _mainMenuCV.gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            LevelProgressionService.OnLevelCompleted += LevelProgressionService_OnLevelCompleted;
        }

        private void OnDisable()
        {
            LevelProgressionService.OnLevelCompleted -= LevelProgressionService_OnLevelCompleted;
        }

        private void LevelProgressionService_OnLevelCompleted(object sender, System.EventArgs e)
        {
            _levelCompletedCV.gameObject.SetActive(true);
        }

        public void CompleteLevel()
        {
            _levelCompletedCV.gameObject.SetActive(false);

            _gridController.CreateNewLevel();
            _agentMoveService.TeleportPlayerToEntrance();
        }

        public void StartLevel()
        {
            _mainMenuCV.gameObject.SetActive(false);
        }
    }
}