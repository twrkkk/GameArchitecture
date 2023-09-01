using Assets.CodeBase.Enemy;
using Assets.CodeBase.Hero;
using Assets.CodeBase.Hud;
using Assets.CodeBase.Infrastructure.States;
using Assets.CodeBase.Logic;
using CodeBase.CameraLogic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using System;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState: IPayloadedState<string>, IExitable
    {
        private const string InitialPointTag = "InitialPoint";
        private const string EnemySpawnerTag = "EnemySpawner";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.CleanUp();
            _sceneLoader.Load(sceneName, onLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void onLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (var progressReaders in _gameFactory.ProgressReaders)
            {
                progressReaders.LoadProgress(_progressService.PlayerProgress);
            }
        }

        private void InitGameWorld()
        {
            InitSpawners();
            Transform InitialPoint = GameObject.FindWithTag(InitialPointTag).transform;
            GameObject hero = _gameFactory.CreateHero(InitialPoint);
            InitHud(hero);
            CameraFollow(hero);
        }

        private void InitSpawners()
        {
            foreach (GameObject spawnObject in GameObject.FindGameObjectsWithTag(EnemySpawnerTag))
            {
                var spawner = spawnObject.GetComponent<EnemySpawner>();
                _gameFactory.Register(spawner);
            }
        }

        private void InitHud(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHud();

            ActorUI actorUI = hud.GetComponentInChildren<ActorUI>();
            HeroHealth health = hero.GetComponent<HeroHealth>();
            actorUI.Construct(health);
        }

        private void CameraFollow(GameObject target) => Camera.main
            .GetComponent<CameraFollow>()
            .Follow(target);

        
    }

}