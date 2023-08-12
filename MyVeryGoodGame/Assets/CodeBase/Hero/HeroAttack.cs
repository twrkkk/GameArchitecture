using Assets.CodeBase.Enemy;
using Assets.CodeBase.Infrastructure.Services;
using CodeBase.Data;
using CodeBase.Hero;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Services.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CodeBase.Hero
{
    public class HeroAttack:MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private CharacterController _characterController;
        private IInputService _inputService;
        private int _layerMask;
        private Collider[] _hits = new Collider[3];
        private Stats _stats;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if(_inputService.IsAttackButtonUp() && _animator.IsAttacking == false)
            {
                _animator.PlayAttack();
            }
        }

        public void OnAttack()
        {
            int hitCount = Hit();
            for (int i = 0; i < hitCount; i++)
            {
                _hits[i].transform.parent.GetComponent<Enemy.IHealth>().TakeDamage(_stats.Damage);
            }
        }

        private int Hit()
        {
            return Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.DamageRadius, _hits, _layerMask);
        }
        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, _characterController.center.y / 2, transform.position.z);

        public void LoadProgress(PlayerProgress progress)
        {
            _stats = progress.HeroStats;
        }
    }
}
