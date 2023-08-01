using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState: IPayloadedState<string>, IExitable
    {
        private const string InitialPointTag = "InitialPoint";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;  

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, onLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void onLoaded()
        {
            Transform InitialPoint = GameObject.FindWithTag(InitialPointTag).transform;
            GameObject hero = _gameFactory.CreateHero(InitialPoint);
            _gameFactory.CreateHud();

            CameraFollow(hero);

            _stateMachine.Enter<GameLoopState>();
        }

        private void CameraFollow(GameObject target) => Camera.main
            .GetComponent<CameraFollow>()
            .Follow(target);

        
    }

}