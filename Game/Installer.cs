using Zenject;
using Pathfinding.Services;
using Pathfinding.Controllers;

namespace Pathfinding.Game
{
    // Installs all game services and controllers into the Zenject dependency container
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Bind non-Monobehaviour game services
            Container.Bind<LevelProgressionService>().AsSingle();
            Container.Bind<PlayerInputService>().AsSingle();
            Container.Bind<LevelUtilityService>().AsSingle();
            Container.Bind<LevelAgentService>().AsSingle();
            Container.Bind<LevelGeneratorService>().AsSingle();
            Container.Bind<AgentPathService>().AsSingle();
            Container.Bind<AgentCategoryService>().AsSingle();
            Container.Bind<OverheadCameraService>().AsSingle();
            Container.Bind<MouseWorldService>().AsSingle();
            Container.Bind<LevelSpawnService>().AsSingle();
            Container.Bind<AgentSpawnService>().AsSingle();
            Container.Bind<AgentMoveService>().AsSingle();
            Container.Bind<AgentAnimationService>().AsSingle();

            // Bind Monobehaviour game services
            Container.Bind<AudioMonobService>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CameraCenteringMonobService>().FromComponentInHierarchy().AsSingle();
            Container.Bind<MenuFadeMonobService>().FromComponentInHierarchy().AsSingle();

            // Bind singleton controllers
            Container.Bind<GameController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UIController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CameraController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<AgentsController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<LevelController>().FromComponentInHierarchy().AsSingle();

            // Bind non-singleton controllers
            Container.Bind<AgentController>().FromComponentInHierarchy().AsTransient();
        }
    }
}
