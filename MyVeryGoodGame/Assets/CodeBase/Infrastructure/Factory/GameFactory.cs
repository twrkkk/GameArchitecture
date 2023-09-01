using Assets.CodeBase.Enemy;
using Assets.CodeBase.Hud;
using Assets.CodeBase.StaticData;
using CodeBase.Infrastructure.Services.PersistentProgress;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private GameObject HeroGameObject { get; set; }

        public GameFactory(IAssetProvider assets, IStaticDataService dataService)
        {
            _assets = assets;
            _staticData = dataService;
        }

        public GameObject CreateHero(Transform initialPoint)
        {
            HeroGameObject = InstantiateAndRegister(AssetPath.HeroPath, initialPoint);

            return HeroGameObject;
        }
        public GameObject CreateHud()
        {
            GameObject hud = InstantiateAndRegister(AssetPath.HudPath);

            return hud;
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateAndRegister(string prefabPath, Transform initialPoint)
        {
            GameObject prefab = _assets.Instantiate(prefabPath, initialPoint.position);
            RegisterProgressWatchers(prefab);
            return prefab;
        }

        private GameObject InstantiateAndRegister(string prefabPath)
        {
            GameObject prefab = _assets.Instantiate(prefabPath);
            RegisterProgressWatchers(prefab);
            return prefab;
        }

        private void RegisterProgressWatchers(GameObject hero)
        {
            foreach (var progressReader in hero.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        public GameObject CreateMonster(MonsterTypeId typeId, Transform parent)
        {
            MonsterStaticData monsterData = _staticData.ForMonster(typeId);
            Debug.Log(monsterData);
            GameObject monster = GameObject.Instantiate(monsterData.Prefab, parent.position, Quaternion.identity, parent);

            var health = monster.GetComponent<IHealth>();
            health.Current = monsterData.Hp;
            health.Max = monsterData.Hp;

            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(HeroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

            var attack = monster.GetComponent<Attack>();
            attack.Construct(HeroGameObject.transform);
            attack.Damage = monsterData.Damage;
            attack.Cleavage = monsterData.Cleavage;
            attack.EffectiveDistance = monsterData.EffectiveDistance;

            monster.GetComponent<RotateToHero>()?.Construct(HeroGameObject.transform);

            return monster;
        }
    }
}