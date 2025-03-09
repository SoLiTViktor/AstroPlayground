using UniRx;
using UnityEngine;
using Zenject;

namespace AsteroidProject
{
    public class LazerDataViewModel
    {
        private LazerGun _lazerGun;
        private LazerDataView _lazerDataView;

        private Color _readyBarColor = Color.white;
        private Color _notReadyBarColor = Color.gray;

        private bool _isRecharging = false;

        [Inject]
        private void Construct(LazerGun lazerGun, LazerDataView lazerDataView)
        {
            _lazerGun = lazerGun;
            _lazerDataView = lazerDataView;

            SubscribeVeiwToPropertyChanges();
        }

        private void SubscribeVeiwToPropertyChanges()
        {
            _lazerGun.CurrentAmmoCount.Subscribe(RedrawAmmoCount);
            _lazerGun.CurrentRechargeTime.Subscribe(RedrawRechargingProgressBar);
        }

        private void RedrawAmmoCount(int currentAmmoCount)
        {
            _lazerDataView.ChangeLaserCountText(currentAmmoCount.ToString() + "/" + _lazerGun.MaxAmmoCount);
        }

        private void RedrawRechargingProgressBar(float rechargingProgress)
        {
            float clumpMaxValue = 1;
            float epsilon = 0.001f;

            if (_lazerGun.CurrentRechargeTime.Value < _lazerGun.RechargeTime)
            {
                if (_isRecharging == false)
                {
                    _isRecharging = true;
                    _lazerDataView.SetRechargingBarColor(_notReadyBarColor);
                }

                float clumpProgress = _lazerGun.CurrentRechargeTime.Value / _lazerGun.RechargeTime;

                _lazerDataView.ChangeProgressBarFillingValue(clumpProgress);

                if (Mathf.Abs(clumpMaxValue - clumpProgress) < epsilon)
                {
                    _isRecharging = false;
                    _lazerDataView.SetRechargingBarColor(_readyBarColor);
                }
            }
        }
    }
}
