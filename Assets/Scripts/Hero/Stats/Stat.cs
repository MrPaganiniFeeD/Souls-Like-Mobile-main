using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Hero.Stats
{
    [Serializable]
    public abstract class Stat
    {
        public event Action StateChanged;
        [SerializeField] private int _currentValue;
        [SerializeField] private int _baseValue;

        public string Name { get; protected set; }
        private int _maxValue;
        public int BaseValue => _baseValue;
        public int MaxValue => _maxValue;
        public int Value
        {
            get => _currentValue;
            set
            {
                if (value > _maxValue)
                {
                    value = _maxValue;
                }
                _currentValue = value;
                StateChanged?.Invoke();
            }
        }


        public readonly ReadOnlyCollection<StatModifier> StatModifiers;
        private readonly List<StatModifier> _statModifiers;
    
        public Stat()
        {
            _statModifiers = new List<StatModifier>();
            StatModifiers = _statModifiers.AsReadOnly();
            Value = _baseValue;
        }

        public Stat(string name) : this()
        {
            Name = name;    
        }

        public Stat(string name, int baseValue, int maxValue) : this()
        {   
            _baseValue = baseValue; 
            _maxValue = maxValue;
            Value = baseValue;
            Name = name;
        }
    
        public Stat(string name,int baseValue) : this()
        {
            _baseValue = baseValue;
            _maxValue = int.MaxValue;
            Value = baseValue;
            Name = name;
        }
        public void AddModifier(StatModifier modifier)
        {
            _statModifiers.Add(modifier);
            _statModifiers.Sort(CompareModifierOrder);
            Value = (int)CalculateFinalValue();
        }
        private int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order)
                return -1; 
            return a.Order > b.Order ? 1 : 0;
        }
        public bool TryRemoveModifier(StatModifier modifier)
        {
            if (_statModifiers.Remove(modifier))
            {
                Value = (int)CalculateFinalValue();
                return true;
            }
            return false;
        }
        public bool TryRemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;

            for (int i = _statModifiers.Count - 1; i >= 0; i--)
            {
                if (_statModifiers[i].Source == source)
                {
                    didRemove = true;
                    _statModifiers.RemoveAt(i);
                    Value = (int)CalculateFinalValue();
                }
            }
            return didRemove;
        }
        private float CalculateFinalValue()
        {
            float finalValue = _baseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < _statModifiers.Count; i++)
            {
                StatModifier modifier = _statModifiers[i];

                switch (modifier.Type)
                {
                    case StatModifierType.Flat:
                        finalValue += modifier.Value;
                        break;
                
                    case StatModifierType.PercentAdd:
                    {
                        sumPercentAdd += modifier.Value;

                        if (i + 1 >= _statModifiers.Count || _statModifiers[i + 1].Type != StatModifierType.PercentAdd)
                        {
                            finalValue *= 1 + sumPercentAdd;
                            sumPercentAdd = 0;
                        }

                        break;
                    }
                
                    case StatModifierType.PercentMult:
                        finalValue *= 1 + modifier.Value;
                        break;
                }
            }
            return (float)Math.Round(finalValue, 4);
        }

        public void SetMaxValue()
        {
            _maxValue = (int)CalculateFinalValue();
            _baseValue = _maxValue;
            StateChanged?.Invoke();
        }
    }
}
