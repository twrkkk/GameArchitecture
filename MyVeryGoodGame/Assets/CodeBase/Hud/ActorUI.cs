using Assets.CodeBase.Enemy;
using Assets.CodeBase.Hero;
using UnityEngine;

namespace Assets.CodeBase.Hud
{
    public class ActorUI : MonoBehaviour
    {
        public HPBar HPBar;
        private IHealth _health;

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();   

            if(health != null) 
            {
                Construct(health);
            }
        }
        private void OnDestroy()
        {
            _health.HealthChanged -= UpdateHPBar;
        }
        public void Construct(IHealth health)
        {
            _health = health;
            _health.HealthChanged += UpdateHPBar;
        }

        private void UpdateHPBar()
        {
            HPBar.SetValue(_health.Current, _health.Max);
        }
    }
}
