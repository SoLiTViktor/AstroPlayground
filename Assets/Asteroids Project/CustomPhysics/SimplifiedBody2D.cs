using UnityEngine;
using Zenject;

namespace AsteroidProject
{
    public class SimplifiedBody2D : MonoBehaviour
    {
        [SerializeField, Min(0.01f)] private float _mass;

        private SimplifiedPhysics2D _physics;

        private Transform _transform;
        private Vector2 _velocity;
        private float _torque;

        private SimplifiedBody2D _collisingBody;

        public Vector2 Velocity => _velocity;
        public float Torque => _torque;
        public float Mass => _mass;

        [Inject]
        private void Construct(SimplifiedPhysics2D simplifiedPhysics2D)
        {
            _physics = simplifiedPhysics2D;
        }

        private void Awake()
        {
            _transform = transform;
            _velocity = Vector2.zero;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out SimplifiedBody2D body))
            {
                _collisingBody = body;
                _collisingBody.AddForce(_physics.CalulateCrushForce(this, _collisingBody));
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (_collisingBody == null)
                return;

            if (_collisingBody.gameObject.activeSelf == false)
            {
                _collisingBody = null;
                return;
            }

            _collisingBody.AddForce(_physics.CalulateCrushForce(this, _collisingBody));
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            float retardingRatio = -0.5f;

            if (collision.TryGetComponent(out SimplifiedBody2D body))
                if (body.GetInstanceID() == _collisingBody?.GetInstanceID())
                {
                    _collisingBody.AddForce(_collisingBody.Velocity * _collisingBody.Mass * retardingRatio);
                    _collisingBody = null;
                }
        }

        public void OnEnable()
        {
            _collisingBody = null;
            _velocity = Vector2.zero;
            _torque = 0;
        }

        private void FixedUpdate()
        {
            UpdateVelocity();
            UpdateTorque();

            _transform.position += new Vector3(_velocity.x, _velocity.y, 0);
            _transform.rotation *= Quaternion.Euler(new Vector3(0, 0, _torque));
        }

        public void AddForce(Vector2 force) => _velocity += force * Time.fixedDeltaTime;

        public void AddTorque(float rotateForce) => _torque += rotateForce * Time.fixedDeltaTime;

        private void UpdateVelocity()
        {
            if (_velocity == Vector2.zero)
                return;

            _velocity = _physics.RetardMoving(_velocity, _mass);
        }

        private void UpdateTorque()
        {
            float epsilon = 0.0005f;

            if (Mathf.Abs(_torque) < epsilon)
                return;

            _torque = _physics.RetardRotation(_torque, _velocity, _mass);
        }
    }
}