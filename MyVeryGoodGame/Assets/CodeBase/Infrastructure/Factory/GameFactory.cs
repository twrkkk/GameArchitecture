using Assets.CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public event Action heroCreated;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameObject HeroGameObject { get; set; }

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public GameObject CreateHero(Transform initialPoint)
        {
            HeroGameObject = InstantiateAndRegister(AssetPath.HeroPath, initialPoint);
            heroCreated?.Invoke();

            return HeroGameObject;
        }
        public GameObject CreateHud()
        {
            return InstantiateAndRegister(AssetPath.HudPath);
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateAndRegister(string prefabPath, Transform initialPoint)
        {
            GameObject hero = _assets.Instantiate(prefabPath, initialPoint.position);
            RegisterProgressWatchers(hero);
            return hero;
        }

        private GameObject InstantiateAndRegister(string prefabPath)
        {
            GameObject hero = _assets.Instantiate(prefabPath);
            RegisterProgressWatchers(hero);
            return hero;
        }

        private void RegisterProgressWatchers(GameObject hero)
        {
            foreach (var progressReader in hero.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }
    }
}