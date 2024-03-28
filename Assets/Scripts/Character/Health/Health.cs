using System;
using UnityEngine;

namespace Homework
{
    [Serializable]
    public class Health : IReadableHealth, IWritableHealth
    {
        public event Action<float> ValueChanged;

        [SerializeField] private float maxValue;

        public float Value { get; private set; }
        public float MaxValue => maxValue;
        public bool IsEmpty => Value == 0;

        private void Set(float value)
        {
            Value = value;
            ValueChanged?.Invoke(value);
        }

        public void Apply(float value)
        {
            Set(Mathf.Clamp(value, 1, maxValue));
        }

        public void Add(float amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("Значение ниже нуля");
            }

            var value = Value + amount;

            Set(value < maxValue ? value : maxValue);
        }

        public void Remove(float amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("Значение ниже нуля");
            }

            var value = Value - amount;

            if (value > 0)
            {
                Set(value);
            }
            else
            {
                Set(0);
            }
        }
    }
}