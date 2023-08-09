using Assets.CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Factory;
using System;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

namespace Assets.CodeBase.Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        public NavMeshAgent Agent;
        private Transform _target;
        private IGameFactory _gameFactory;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.HeroGameObject != null)
                InitializeHeroTransform();
            else
                _gameFactory.heroCreated += HeroCreated;
        }

        private void InitializeHeroTransform()
        {
            _target = _gameFactory.HeroGameObject.transform;
        }

        private void HeroCreated()
        {
            InitializeHeroTransform();
        }

        private void Update()
        {
            if (_target != null)
                Agent.destination = _target.position;
        }
    }
}
