using System;
using UnityEngine;
using Zenject;

namespace AsteroidProject
{
    public class DesktopInput : IInput, ITickable
    {
        private const string HorizontalInput = "Horizontal";
        private const string VerticalInput = "Vertical";

        private Vector2 _axisInput;
        private Vector2 _previousAxisInput = Vector2.zero;

        public event Action<Vector2> AxisButtonsDown;
        public event Action AttackButtonDown;
        public event Action SuperAttackButtonDown;


        public void Tick()
        {
            ReadAxisButtonsClick();
            ReadButtonsClick();
        }

        private void ReadButtonsClick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                AttackButtonDown?.Invoke();
            else if (Input.GetKeyDown(KeyCode.V))
                SuperAttackButtonDown?.Invoke();
        }

        private void ReadAxisButtonsClick()
        {
            _axisInput = new Vector2(Input.GetAxis(HorizontalInput), Input.GetAxis(VerticalInput));

            if (_axisInput != Vector2.zero || _previousAxisInput != Vector2.zero)
                AxisButtonsDown?.Invoke(_axisInput);

            _previousAxisInput = _axisInput;
        }
    }
}
