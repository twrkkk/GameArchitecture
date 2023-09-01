using Assets.CodeBase.Hero;
using Assets.CodeBase.Infrastructure.Services;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.CodeBase.Enemy
{
    public class Attack : MonoBehaviour
    {
        public EnemyAnimator Animator;
        public float AttackCooldown = 3f;
        public float EffectiveDistance = 0.5f;
        public float Cleavage = 0.5f;
        public float Damage = 10f;

        private IGameFactory _gameFactory;
        private Transform _hero;
        private float _attackCooldownTimer;
        private bool _attackEnded = true;
        private Collider[] _hits = new Collider[1];
        private int _layerMask;
        private bool _attackIsActive;
        public void Construct(Transform target)
        {
            _hero = target;
        }

        private void Start()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            if (IsCooldown())
                _attackCooldownTimer -= Time.deltaTime;
            else if (CanAttack())
                StartAttack();
        }

        private bool CanAttack() =>
            _attackEnded && _attackIsActive;

        public void OnAttack() // calls into enemy attack animation
        {
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebug(HitPointPosition(), Cleavage, 1f);
                hit.gameObject.GetComponent<IHealth>().TakeDamage(Damage);
            }
        }

        private bool Hit(out Collider hit)
        {
            Vector3 position = HitPointPosition();
            int hitsCount = Physics.OverlapSphereNonAlloc(position, Cleavage, _hits, _layerMask);
            hit = _hits.FirstOrDefault();

            return hitsCount > 0;
        }

        private Vector3 HitPointPosition()
        {
            return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * EffectiveDistance;
        }

        public void OnAttackEnded()
        {
            _attackCooldownTimer = AttackCooldown;
            _attackEnded = true;
        }

        private bool IsCooldown()
        {
            return _attackCooldownTimer > 0;
        }

        private void StartAttack()
        {
            transform.LookAt(_hero.transform);
            Animator.PlayAttack();
            _attackEnded = false;
        }

        public void EnableAttack()
        {
            _attackIsActive = true;
        }

        public void DisableAttack()
        {
            _attackIsActive = false;
        }
    }
}
