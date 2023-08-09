using CodeBase.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.CodeBase.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyAnimator))]
    public class AnimateAlongAgent:MonoBehaviour
    {
        [SerializeField] private float _minimalVelocity = 0.1f;

        public NavMeshAgent Agent;
        public EnemyAnimator Animator;

        private void Update()
        {
            MoveAnimation();
        }

        private void MoveAnimation()
        {
            float speed = Agent.velocity.magnitude;
            if (speed > _minimalVelocity)
                Animator.Move(speed);
            else
                Animator.StopMoving();
        }
    }
}
