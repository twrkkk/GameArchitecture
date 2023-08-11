using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CodeBase.Enemy
{
    public class CheckAttackRange:MonoBehaviour
    {
        public Attack Attack;
        public TriggerObserver TriggerObserver;

        private void Start()
        {
            TriggerObserver.TriggerEnter += TriggerEnter;
            TriggerObserver.TriggerExit += TriggerExit;
            Attack.DisableAttack();
        }

        private void TriggerEnter(Collider obj)
        {
            Attack.EnableAttack();
        }
        private void TriggerExit(Collider obj)
        {
            Attack.DisableAttack();
        }
    }
}
