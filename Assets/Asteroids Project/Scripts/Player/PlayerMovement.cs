using UniRx;
using UnityEngine;
using Zenject;

namespace AsteroidProject
{
    public class PlayerMovement : MonoBehaviour
    {
        private Transform _playerTransform;
        private SimplifiedBody2D _playerBody2D;

        private float _movingSpeed;
        private float _rotationSpeed;

        public readonly ReactiveProperty<float> InstantSpeed = new();
        public readonly ReactiveProperty<Vector3> Position = new();
        public readonly ReactiveProperty<float> Rotation = new();

        [Inject]
        private void Construct(GameCore gameCore)
        {
            _movingSpeed = gameCore.GameCoreData.PlayerData.MovingSpeed;
            _rotationSpeed = gameCore.GameCoreData.PlayerData.RotationSpeed;
        }

        private void Awake()
        {
            _playerBody2D = GetComponent<SimplifiedBody2D>();
            _playerTransform = transform;
        }

        private void Update() => UpdateReactiveProperties();

        public void Move(Vector2 axisInput)
        {
            Vector2 moveForce = _playerTransform.up * axisInput.y * _movingSpeed;
            _playerBody2D.AddForce(moveForce);
        }

        public void Rotate(Vector2 axixInput)
        {
            float torqueInMovingFactor = 0.5f;
            float torque = -axixInput.x * _rotationSpeed;

            if (axixInput.y > 0)
                torque *= torqueInMovingFactor;

            _playerBody2D.AddTorque(torque);
        }


        private void UpdateReactiveProperties()
        {
            if (InstantSpeed.Value != _playerBody2D.Velocity.magnitude)
                InstantSpeed.Value = _playerBody2D.Velocity.magnitude;

            if (Position.Value != _playerTransform.position)
                Position.Value = _playerTransform.position;

            if (Rotation.Value != _playerTransform.eulerAngles.z)
                Rotation.Value = _playerTransform.eulerAngles.z;
        }
    }
}
