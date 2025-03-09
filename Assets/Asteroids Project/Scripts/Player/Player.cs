using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace AsteroidProject
{
    [RequireComponent(typeof(SimplifiedBody2D))]
    public class Player : MonoBehaviour, IDamageable, IPlayer
    {
        [SerializeField] private int _ignoreCollisionLayerIndex = 8;

        [Header("Device's Pivots")]
        [SerializeField] private Transform _weaponPositionPivot;
        [SerializeField] private Transform _leftJetPivot;
        [SerializeField] private Transform _rightJetPivot;
        [SerializeField] private Transform _centralJetPivot;

        private PlayerLives _lives;
        private PlayerMovement _playerMovement;
        private PlayerWeaponry _weaponry;
        private PlayerEffectViewer _effectViewer;

        private SignalBus _signalBus;
        private IInput _playerInput;

        private int _playingLayer;

        private bool _isStunned;
        private float _stunDuration;
        private float _shieldDuration;

        public Transform Transform => transform;

        [Inject]
        private void Construct(
            GameCore gameCore, PlayerLives lives, PlayerWeaponry weaponry, PlayerEffectViewer effectViewer,
            IInput input, SignalBus signalBus)
        {
            _signalBus = signalBus;
            _playerInput = input;
            _lives = lives;
            _weaponry = weaponry;
            _effectViewer = effectViewer;

            SetDurations(gameCore);
            SetWeaponsPosition();
            SetEffectsPosition();
        }

        private void SetDurations(GameCore gameCore)
        {
            _stunDuration = gameCore.GameCoreData.PlayerData.StunDuration;
            _shieldDuration = gameCore.GameCoreData.PlayerData.ShieldDuration;
        }

        private void Awake()
        {
            _isStunned = false;
            _playingLayer = gameObject.layer;

            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void OnEnable()
        {
            _playerInput.AxisButtonsDown += HandleInput;
            _playerInput.AttackButtonDown += BasicShoot;
            _playerInput.SuperAttackButtonDown += MightShoot;

            _lives.Dead += Crushed;
            _lives.Damaged += GetStun;
        }

        private void OnDisable()
        {
            _playerInput.AxisButtonsDown -= HandleInput;
            _playerInput.AttackButtonDown -= BasicShoot;
            _playerInput.SuperAttackButtonDown -= MightShoot;
        }

        public async void TakeDamage()
        {
            if (_isStunned)
                return;

            _lives.TakeDamage();
        }

        private void SetWeaponsPosition()
        {
            _weaponry.SetWeaponPosiotion(_weaponPositionPivot);
        }

        private void SetEffectsPosition()
        {
            _effectViewer.SetEffectPosition(Transform, _centralJetPivot, _leftJetPivot, _rightJetPivot);
        }

        private async void GetStun()
        {
            await UniTask.WhenAll(Stun(), ActivateShield());
        }

        private void Crushed()
        {
            _signalBus.Fire(new PlayerCrushedSignal(this));
            gameObject.SetActive(false);
        }

        private void HandleInput(Vector2 axisInput)
        {
            if (_isStunned)
                return;

            if (axisInput.y > 0)
                _playerMovement.Move(axisInput);

            if (axisInput.x != 0)
                _playerMovement.Rotate(axisInput);

            _effectViewer.PlayMovingEffects(axisInput);
        }

        private void BasicShoot()
        {
            if (_isStunned)
                return;

            _weaponry.BasicShoot();
        }

        private void MightShoot()
        {
            if (_isStunned)
                return;

            _weaponry.MightShoot();
        }

        private async UniTask ActivateShield()
        {
            _effectViewer.ActivateShield();
            gameObject.layer = _ignoreCollisionLayerIndex;

            await UniTask.Delay(TimeSpan.FromSeconds(_shieldDuration));

            gameObject.layer = _playingLayer;
        }

        private async UniTask Stun()
        {
            _isStunned = true;
            _effectViewer.StopMovingEffects();

            await UniTask.Delay(TimeSpan.FromSeconds(_stunDuration));

            _isStunned = false;
        }
    }
}