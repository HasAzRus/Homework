using UnityEngine;
using UnityEngine.InputSystem;

namespace Homework
{
    public class InputHandler : Behaviour
    {
        [SerializeField] private InputActionReference _movementJoystick;
        [SerializeField] private InputActionReference _shootButton;

        private bool _isShooting;

        private IControllable _controllable;

        protected override void Update()
        {
            base.Update();

            if (_controllable == null)
            {
                return;
            }
            
            _controllable.Move(_movementJoystick.action.ReadValue<Vector2>());

            if (_shootButton.action.IsPressed())
            {
                _isShooting = true;
                
                _controllable.Shoot();
            }

            if (!_shootButton.action.IsPressed())
            {
                if (_isShooting)
                {
                    _isShooting = false;
                    
                    _controllable.StopShooting();
                }
            }
        }
        
        public void Control(IControllable controllable)
        {
            _controllable = controllable;
        }

        public void StopControlling()
        {
            _controllable = null;
        }
    }
}