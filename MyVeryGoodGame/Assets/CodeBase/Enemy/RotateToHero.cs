
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
        public void Construct(Transform target)
        {
            _target = target;
        }

        private void Update()
        {
            RotateToTarget();
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

        private float SpeedFactor() =>
            Speed * Time.deltaTime;
    }
}
