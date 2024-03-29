using UnityEngine;

namespace Homework
{
    public class Healthbar : Behaviour
    {
        [SerializeField] private float _scaleAmount;
        [SerializeField] private Transform _barTransform;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Gradient _gradient;

        [SerializeField] private Character _target;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _target.ReadableHealth.ValueChanged += OnValueChanged;
        }

        private void OnValueChanged(float value)
        {
            var normalizedValue = value / _target.ReadableHealth.MaxValue;
            
            var scale = Vector3.one;
            scale.x = normalizedValue * _scaleAmount;

            _spriteRenderer.color = _gradient.Evaluate(normalizedValue);
            
            _barTransform.localScale = scale;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _target.ReadableHealth.ValueChanged -= OnValueChanged;
        }
    }
}