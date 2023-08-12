using System;

namespace CodeBase.Data
{
    [Serializable]
    public class State
    {
        public float MaxHp;
        public float CurrentHp;
        public void ResetHP() => CurrentHp = MaxHp;
    }
}