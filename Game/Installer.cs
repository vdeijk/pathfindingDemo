using Zenject;
using Pathfinding.Services;
using Pathfinding.Controllers;

namespace Pathfinding.Game
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //Game
            Container.Bind<GameController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<LevelProgressionService>().AsSingle();
            Container.Bind<InputService>().AsSingle();

            //Services
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

            //Singleton Controllers
            Container.Bind<UIController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CameraController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<AIController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<AgentsController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GridController>().FromComponentInHierarchy().AsSingle();

            // Transient controllers
            Container.Bind<AgentController>().FromComponentInHierarchy().AsTransient();

        }
    }
}
