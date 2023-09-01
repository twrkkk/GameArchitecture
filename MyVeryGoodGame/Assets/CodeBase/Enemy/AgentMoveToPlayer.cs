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

        public void Construct(Transform target)
        {
            _target = target;
        }

        private void Update()
        {
            if (_target != null)
                Agent.destination = _target.position;
        }
    }
}
