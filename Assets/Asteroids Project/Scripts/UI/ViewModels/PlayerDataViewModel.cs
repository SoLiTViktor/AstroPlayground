using UniRx;
using UnityEngine;
using Zenject;

namespace AsteroidProject
{
    public class PlayerDataViewModel
    {
        private PlayerMovement _playerMovement;
        private PlayerDataView _playerDataView;

        private string formattedValue = "0.00";

        [Inject]
        private void Construct(PlayerMovement playerMovement, PlayerDataView playerDataView)
        {
            _playerMovement = playerMovement;
            _playerDataView = playerDataView;

            SubscribeVeiwToPropertyChanges();
        }

        private void SubscribeVeiwToPropertyChanges()
        {
            _playerMovement.Position.Subscribe(RedrawPosition);
            _playerMovement.Rotation.Subscribe(RedrawRotation);
            _playerMovement.InstantSpeed.Subscribe(RedrawSpeed);
        }

        private void RedrawPosition(Vector3 position)
        {
            _playerDataView.ChangePositionText(position.x.ToString(formattedValue) + " : " + position.y.ToString(formattedValue));
        }

        private void RedrawRotation(float rotation)
        {
            _playerDataView.ChangeRotationText(rotation.ToString(formattedValue));
        }

        private void RedrawSpeed(float speed)
        {
            float grade = 100;
            _playerDataView.ChangeSpeedText((speed * grade).ToString(formattedValue));
        }
    }
}
