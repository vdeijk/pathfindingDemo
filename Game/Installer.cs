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
            // Bind core game controller and progression services
            Container.Bind<GameController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<LevelProgressionService>().AsSingle();
            Container.Bind<InputService>().AsSingle();

            // Bind all game services as singletons
            Container.Bind<LevelUtilityService>().AsSingle();
            Container.Bind<GridAgentService>().AsSingle();
            Container.Bind<LevelGeneratorService>().AsSingle();
            Container.Bind<AgentPathService>().AsSingle();
            Container.Bind<AgentCategoryService>().AsSingle();
            Container.Bind<OverheadCameraService>().AsSingle();
            Container.Bind<MouseWorldService>().AsSingle();
            Container.Bind<PropSpawnService>().AsSingle();
            Container.Bind<AgentSpawnService>().AsSingle();
            Container.Bind<AgentMoveService>().AsSingle();
            Container.Bind<AgentAnimationService>().AsSingle();
            Container.Bind<AudioMonobService>().FromComponentInHierarchy().AsSingle();

            // Bind singleton controllers (one instance per scene)
            Container.Bind<UIController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CameraController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<AIController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<AgentsController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GridController>().FromComponentInHierarchy().AsSingle();

            // Bind AgentController as transient (multiple instances allowed)
            Container.Bind<AgentController>().FromComponentInHierarchy().AsTransient();
        }
    }
}
