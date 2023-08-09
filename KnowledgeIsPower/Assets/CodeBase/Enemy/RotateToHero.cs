
using Assets.CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Factory;
using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Assets.CodeBase.Enemy
{
    public class RotateToHero : Follow
    {
        public float Speed;
        private Transform _target;
        private IGameFactory _gameFactory;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (HeroExists())
            {
                InitHeroTarget();
            }
            else
            {
                _gameFactory.heroCreated += InitHeroTarget;
            }
        }

        private void Update()
        {
            if (Initialized())
            {
                RotateToTarget();
            }
        }

        private void RotateToTarget()
        {
            Vector3 direction = _target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            Quaternion rotation = SmoothRotation(targetRotation);
            transform.rotation = rotation;
        }

        private Quaternion SmoothRotation(Quaternion targetRotation) =>
            Quaternion.Lerp(transform.rotation, targetRotation, SpeedFactor());

        private bool Initialized() =>
            _target != null;

        private float SpeedFactor() =>
            Speed * Time.deltaTime;

        private bool HeroExists()
        {
            return _gameFactory.HeroGameObject != null;
        }

        private void InitHeroTarget()
        {
            _target = _gameFactory.HeroGameObject.transform;
        }
    }
}
