using UnityEngine;
using CodeBase.Services.Input;
using Assets.CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Factory;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState, IExitable
    {
        private const string Initial = "Initial";
        public static IInputService InputService;
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }
        public void Enter()
        {
            _sceneLoader.Load(Initial, EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>("Main");
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(ChooseInputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssetProvider>()));
        }

        private static IInputService ChooseInputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }
    }
}