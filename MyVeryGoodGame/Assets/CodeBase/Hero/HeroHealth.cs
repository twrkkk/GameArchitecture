using Assets.CodeBase.Enemy;
using Assets.CodeBase.Infrastructure.States;
using CodeBase.Data;
using CodeBase.Hero;
using CodeBase.Infrastructure.Services.PersistentProgress;
using System;
using UnityEngine;

namespace Assets.CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        public event Action HealthChanged;
        public float Max
        {
            get => _state.MaxHp;
            set => _state.MaxHp = value;
        }

        public float Current
        {
            get => _state.CurrentHp;
            set
            {
                if (_state.CurrentHp != value)
                {
                    _state.CurrentHp = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public HeroAnimator Animator;
        private State _state;

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HeroState;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.MaxHp = Max;
            progress.HeroState.CurrentHp = Current;
        }

        public void TakeDamage(float value)
        {
            if (Current <= 0) return;

            Current -= value;
            Animator.PlayHit();
        }
    }
}
