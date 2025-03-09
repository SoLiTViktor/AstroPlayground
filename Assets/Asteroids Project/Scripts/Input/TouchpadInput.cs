using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AsteroidProject
{
    public class TouchpadInput : IInput, ITickable, IDisposable
    {
        private DynamicJoystick _joystick;
        private Button _basicShootButton;
        private Button _mightShootButton;


        private Vector2 _axisInput;
        private Vector2 _previousAxisInput = Vector2.zero;


        public event Action<Vector2> AxisButtonsDown;
        public event Action AttackButtonDown;
        public event Action SuperAttackButtonDown;

        [Inject]
        private void Constrcut(TouchPadCanvasView canvas)
        {
            _joystick = canvas.Joystick;
            _basicShootButton = canvas.BasicShootButton;
            _mightShootButton = canvas.MightShootButton;

            SubscribeToButtonActions();
        }

        public void Tick()
        {
            ReadAxisButtonsClick();
        }

        public void Dispose()
        {
            _basicShootButton.onClick.RemoveAllListeners();
            _mightShootButton.onClick.RemoveAllListeners();
        }

        private void SubscribeToButtonActions()
        {
            _basicShootButton.onClick.AddListener(() => AttackButtonDown?.Invoke());
            _mightShootButton.onClick.AddListener(() => SuperAttackButtonDown?.Invoke());
        }

        private void ReadAxisButtonsClick()
        {
            _axisInput = new Vector2(_joystick.Horizontal, _joystick.Vertical);

            if (_axisInput != Vector2.zero || _previousAxisInput != Vector2.zero)
                AxisButtonsDown?.Invoke(_axisInput);

            _previousAxisInput = _axisInput;
        }

    }
}
