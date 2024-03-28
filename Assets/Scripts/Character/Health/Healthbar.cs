using UnityEngine;
using UnityEngine.Serialization;

namespace Homework
{
    public class Healthbar : Behaviour
    {
        [SerializeField] private float _scaleAmount;
        [SerializeField] private Transform _barTransform;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Gradient _gradient;

        [SerializeField] private Character _character;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _character.ReadableHealth.ValueChanged += OnValueChanged;
        }

        private void OnValueChanged(float value)
        {
            var normalizedValue = value / _character.ReadableHealth.MaxValue;
            
            var scale = Vector3.one;
            scale.x = normalizedValue * _scaleAmount;

            _spriteRenderer.color = _gradient.Evaluate(normalizedValue);
            
            _barTransform.localScale = scale;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _character.ReadableHealth.ValueChanged -= OnValueChanged;
        }
    }
}