using Assets.CodeBase.Enemy;
using CodeBase.Hero;
using System;
using UnityEngine;

namespace Assets.CodeBase.Hero
{
    public class HeroDeath:MonoBehaviour
    {
        [SerializeField] private HeroHealth _health;
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private HeroMove _move;
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private GameObject _deathFx;
        private bool _isDead;

        private void Start()
        {
            _health.HealthChanged += HealthChanged;
        }
        private void OnDestroy()
        {
            _health.HealthChanged -= HealthChanged;
        }

        private void HealthChanged()
        {
            if (_isDead == false && _health.Current <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            _move.enabled = false;
            _attack.enabled = false;    
            _animator.PlayDeath();
            Instantiate(_deathFx, transform.position, Quaternion.identity);
        }
    }
}
