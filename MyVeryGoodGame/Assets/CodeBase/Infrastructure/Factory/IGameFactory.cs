using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.StaticData;
using CodeBase.Infrastructure.Services.PersistentProgress;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        void CleanUp();
        GameObject CreateHero(Transform initialPoint);
        GameObject CreateHud();
        GameObject CreateMonster(MonsterTypeId monsterTypeId, Transform parent);
        void Register(ISavedProgressReader progressReader);
    }
}