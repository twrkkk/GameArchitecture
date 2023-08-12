using CodeBase.Enemy;
using System;
using UnityEngine;

namespace Assets.CodeBase.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float current;
        [SerializeField] private float max;

        public float Current { get => current; set => current = value; }
        public float Max { get => max; set => max = value; }

        public event Action HealthChanged;

        public void TakeDamage(float damage)
        {
            Current -= damage;
            _animator.PlayHit();
            HealthChanged?.Invoke();
        }
    }
}
