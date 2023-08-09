using Assets.CodeBase.Infrastructure.Services;
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

        GameObject HeroGameObject { get; set; }

        event Action heroCreated;

        void CleanUp();
        GameObject CreateHero(Transform initialPoint);
        GameObject CreateHud();
    }
}