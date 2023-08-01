using Assets.CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public GameObject CreateHero(Transform initialPoint)
        {
            return _assets.Instantiate(AssetPath.HeroPath, initialPoint.position);
        }

        public GameObject CreateHud()
        {
            return _assets.Instantiate(AssetPath.HudPath);
        }
    }
}