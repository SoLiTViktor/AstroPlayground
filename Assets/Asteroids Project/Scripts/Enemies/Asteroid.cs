using System.Linq;
using UnityEngine;
using Zenject;

namespace AsteroidProject
{
    [RequireComponent(typeof(SimplifiedBody2D))]
    public abstract class Asteroid : Enemy, IPoolable
    {
        [SerializeField] private Transform _model;

        protected SimplifiedBody2D _body;

        protected Vector2 _direction;
        protected Vector3 _torque;

        protected float _permanentSpeed;
        protected float _directionMagnitude;

        [Inject]
        protected void Construct(GameCore gameCore)
        {
            SetType();

            if (gameCore.GameCoreData.AsteroidData.Any(asteroid => asteroid.EnemyType == Type))
            {
                AsteroidStruct asteroidStruct = gameCore.GameCoreData.AsteroidData.First(asteroid => asteroid.EnemyType == Type);
                _permanentSpeed = asteroidStruct.PermanentSpeed;
            }
        }

        private void Awake()
        {
            _body = GetComponent<SimplifiedBody2D>();
            _model = GetComponentInChildren<Transform>();

            _direction = Vector2.zero;
            _torque = Vector3.zero;
        }

        private void OnDisable()
        {
            _direction = Vector2.zero;
            _torque = Vector3.zero;
        }

        private void Update()
        {
            Move();
            RotateModel();
        }


        public void SetMovingDiraction(Vector2 force)
        {
            _direction = force;
            _directionMagnitude = _direction.magnitude;

            AddStartImpulse();
        }

        public void Set3DRotation(Vector3 torque)
        {
            _torque = torque;
        }

        protected override abstract void SetType();

        protected override void Explode()
        {
            base.Explode();
        }

        private void AddStartImpulse()
        {
            _body.AddForce(_direction.normalized);
        }

        private void Move()
        {
            if (AreCodirected(_body.Velocity, _direction) == false)
                _direction = _body.Velocity.normalized * _directionMagnitude;

            _body.AddForce(_direction * _permanentSpeed);
        }

        private void RotateModel()
        {
            _model.transform.rotation *= Quaternion.Euler(_torque * Time.fixedDeltaTime);
        }

        private bool AreCodirected(Vector2 first, Vector2 second)
        {
            float epsilon = 0.001f;

            return Vector2.Dot(first.normalized, second.normalized) > epsilon;

        }
    }
}