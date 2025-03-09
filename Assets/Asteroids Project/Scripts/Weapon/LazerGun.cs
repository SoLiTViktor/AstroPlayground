using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

namespace AsteroidProject
{
    public class LazerGun : Weapon
    {
        private GameObjectPool<Lazer> _lazerPool;

        private int _maxAmmoCount;
        private int _cuurentAmmoCount;

        bool _isRecharging = false;

        public int MaxAmmoCount => _maxAmmoCount;

        public ReactiveProperty<int> CurrentAmmoCount = new();
        public ReactiveProperty<float> CurrentRechargeTime = new();
        public float RechargeTime { get; private set; }

        [Inject]
        private void Construct(GameObjectPool<Lazer> pool, GameCore gameCore)
        {
            _lazerPool = pool;
            _maxAmmoCount = gameCore.GameCoreData.WeaponData.LazerGunMaxAmmoCount;
            RechargeTime = gameCore.GameCoreData.WeaponData.LazerGunRecharge;

            _cuurentAmmoCount = _maxAmmoCount;
            CurrentAmmoCount.Value = _maxAmmoCount;

            CurrentRechargeTime.Value = RechargeTime;

            WeaponType = WeaponType.LazerGun;
        }

        public override void Shoot()
        {
            if (_cuurentAmmoCount == 0)
                return;

            Lazer lazer;

            if (_lazerPool.TryGet(out lazer))
            {
                lazer.transform.SetParent(WeaponPositionPivot);
                lazer.transform.localPosition = Vector3.zero;
                lazer.transform.localRotation = Quaternion.identity;

                _cuurentAmmoCount--;
                CurrentAmmoCount.Value = _cuurentAmmoCount;

                if (_isRecharging == false)
                    Recharge();
            }
        }

        private async void Recharge()
        {
            _isRecharging = true;

            while (_cuurentAmmoCount < _maxAmmoCount)
            {
                CurrentRechargeTime.Value = 0;

                while (CurrentRechargeTime.Value < RechargeTime)
                {
                    CurrentRechargeTime.Value += Time.deltaTime;
                    await UniTask.Yield();
                }

                _cuurentAmmoCount++;
                CurrentAmmoCount.Value = _cuurentAmmoCount;
            }

            _isRecharging = false;
        }
    }
}
