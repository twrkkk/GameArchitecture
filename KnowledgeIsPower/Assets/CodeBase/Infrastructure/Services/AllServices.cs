using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Factory;
using System;
using UnityEditorInternal;

namespace Assets.CodeBase.Infrastructure.Services
{
    public class AllServices
    {
        private static AllServices _instance;
        public static AllServices Container => _instance ?? (_instance = new AllServices());

        public void RegisterSingle<TService>(TService implementation) where TService: IService
        {
            Implementation<TService>.Instance = implementation;
        }

        public TService Single<TService>() where TService : IService
        {
            return Implementation<TService>.Instance;
        }
    }

    public class Implementation<TService> where TService : IService
    {
        public static TService Instance;
    }
}
