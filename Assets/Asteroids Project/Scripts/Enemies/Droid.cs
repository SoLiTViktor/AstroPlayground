using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;

namespace AsteroidProject
{
    [RequireComponent(typeof(SimplifiedBody2D))]
    public class Droid : Enemy, IPoolable
    {
        [SerializeField] private Transform _model;

        private SimplifiedBody2D _body;
        private Transform _target;

        private float _moveSpeed;
        private float _rotateSpeed;
        private float _modelTorque;

        private int _suicideDelay;

        [Inject]
        private void Construct(GameCore gameCore)
        {
            SetMovingValues(gameCore);
            SetSelfExplodeDelay(gameCore);      
        }

        private void Awake()
        {
            _body = GetComponent<SimplifiedBody2D>();
            Type = EnemyType.Droid;
        }

        private async void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IPlayer _))
            {
                await UniTask.Delay(_suicideDelay);
                SelfExplode();
            }
        }

        private void OnDisable()
        {
            _target = null;
        }

        private void Update()
        {
            LookToTarget();
            Move();
            ZAxisRotate();
        }

        public void StartMoveToTarget(Transform target)
        {
            _target = target;
            _body.AddForce(transform.up);
        }

        public override void TakeDamage()
        {
            Explode();
        }

        protected override void Explode()
        {
            base.Explode();
        }

        protected override void SetType()
        {
            Type = EnemyType.Droid;
        }

        private void SetMovingValues(GameCore gameCore)
        {
            _moveSpeed = gameCore.GameCoreData.DroidData.MoveSpeed;
            _rotateSpeed = gameCore.GameCoreData.DroidData.RotateSpeed;
            _modelTorque = gameCore.GameCoreData.DroidData.ModelTorque;
        }

        private void SetSelfExplodeDelay(GameCore gameCore)
        {
            _suicideDelay = gameCore.GameCoreData.DroidData.SelfExplodeDelayMilliseconds;
        }

        private void LookToTarget()
        {
            int rotationOffset = -90;
            Vector3 difference = _target.position - transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(rotationZ + rotationOffset, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * _rotateSpeed);
        }

        private void Move()
        {
            _body.AddForce(transform.up * _moveSpeed);
        }

        private void ZAxisRotate()
        {
            _model.transform.rotation *= Quaternion.Euler(Vector3.forward * _modelTorque * Time.fixedDeltaTime);
        }

        private void SelfExplode()
        {
            SignalBus.Fire(new DroidSelfExplodedSignal(this));
            gameObject.SetActive(false);
        }
    }
}