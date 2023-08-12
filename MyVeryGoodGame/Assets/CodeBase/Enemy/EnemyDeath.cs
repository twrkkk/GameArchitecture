using CodeBase.Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CodeBase.Enemy
{
    public class EnemyDeath:MonoBehaviour
    {
        public EnemyHealth Health;
        public EnemyAnimator Animator;
        public GameObject DeathFx;

        public event Action OnDeath;

        private void Start()
        {
            Health.HealthChanged += HealthChanged;
        }

        private void OnDestroy()
        {
            Health.HealthChanged -= HealthChanged;
            
        }

        private void HealthChanged()
        {
            if (Health.Current <= 0)
                Die();
        }

        private void Die()
        {
            Health.HealthChanged -= HealthChanged;

            Animator.PlayDeath();
            SpawnDeathFx();
            StartCoroutine(DestroyTimer());

            OnDeath?.Invoke();
        }

        private void SpawnDeathFx()
        {
            Instantiate(DeathFx, transform.position, Quaternion.identity);
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
    }
}
