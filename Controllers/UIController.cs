using Pathfinding.Services;
using System.Drawing.Text;
using UnityEngine;
using Zenject;

namespace Pathfinding.Controllers
{
    [DefaultExecutionOrder(-100)]
    public class UIController : MonoBehaviour
    {
        [SerializeField] CanvasGroup levelCompletedCV;
        [SerializeField] CanvasGroup mainMenuCV;

        [Inject] private AgentMoveService _agentMoveService;
        [Inject] private GridController _gridController;

        private void Awake()
        {
            levelCompletedCV.gameObject.SetActive(false);
            mainMenuCV.gameObject.SetActive(true);
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
            levelCompletedCV.gameObject.SetActive(true);
        }

        public void CompleteLevel()
        {
            levelCompletedCV.gameObject.SetActive(false);

            _gridController.CreateNewLevel();
            _agentMoveService.TeleportPlayerToEntrance();
        }

        public void StartLevel()
        {
            mainMenuCV.gameObject.SetActive(false);
        }
    }
}