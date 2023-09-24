using System;
using System.Collections;
using PlayerLogic.Stats;
using UnityEngine;

namespace DefaultNamespace.Stats
{
    [Serializable]
    public class Stamina : Stat, IUsingStat
    {
        [SerializeField] private float _timeRegenTick;
        [SerializeField] private float _timeDelayBeforeRegen;
        
        private Coroutine _regenStamina;
        private WaitForSeconds _regenTick;

        public Stamina(string name, int baseValue, int maxValue, float timeRegenTick, float timeDelayBeforeRegen) :
            base(name, baseValue, maxValue)
        {
            _timeRegenTick = timeRegenTick;
            _timeDelayBeforeRegen = timeDelayBeforeRegen;
            _regenTick = new WaitForSeconds(_timeRegenTick);
        }
        public Stamina(string name) : base(name){}
        public Stamina(string name,int baseValue) : base(name,baseValue){}
        
        public bool TryUse(int costValue)
        {
            if (Value - costValue >= 0)
            {
                Value -= costValue;
                
                if(_regenStamina != null)
                    Coroutines.StopCoroutines(_regenStamina);
                    
                _regenStamina = Coroutines.StartCoroutines(RegenStamina());
                return true;
            }
            return false;
        }

        public bool CheckValue(int value)
        {
            return Value - value >= 0;
        }

        private IEnumerator RegenStamina()
        {
            yield return new WaitForSeconds(_timeDelayBeforeRegen);
            float templateValue = Value;
            
            while (Value < MaxValue)
            {
                templateValue += MaxValue / 100f;
                Value = (int) templateValue;
                yield return _regenTick;
            }

            _regenStamina = null;
        }
    }
}