using System;
using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.Enemy
{
    public class Aggro:MonoBehaviour
    {
        public TriggerObserver TriggerObserver;
        public Follow Follow;
        public float Cooldown;
        private Coroutine _aggroCoroutine;
        private bool _hasAggroTarget;

        private void Start()
        {
            TriggerObserver.TriggerEnter += TriggerEnter;
            TriggerObserver.TriggerExit += TriggerExit;

            SwitchFollowActive(false);
        }

        private void OnDisable()
        {
            TriggerObserver.TriggerEnter -= TriggerEnter;
            TriggerObserver.TriggerExit -= TriggerExit;
        }
        private void TriggerEnter(Collider obj)
        {
            if (_hasAggroTarget) return;
            _hasAggroTarget = true;
            StopAggroCoroutine();
            SwitchFollowActive(true);
        }
        private void TriggerExit(Collider obj)
        {
            if (!_hasAggroTarget) return;
            _hasAggroTarget = false;
            _aggroCoroutine = StartCoroutine(SwitchFollowAfterCooldown());
        }

        private void StopAggroCoroutine()
        {
            if (_aggroCoroutine == null) return;
            StopCoroutine(_aggroCoroutine);
            _aggroCoroutine = null; 
        }

        private void SwitchFollowActive(bool value)
        {
            Follow.enabled = value;
        }

        private IEnumerator SwitchFollowAfterCooldown()
        {
            yield return new WaitForSecondsRealtime(Cooldown);

            SwitchFollowActive(false);
        }
    }
}
