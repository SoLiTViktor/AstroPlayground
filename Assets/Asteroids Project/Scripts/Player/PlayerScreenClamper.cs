using UnityEngine;
using Zenject;

namespace AsteroidProject
{
    public class PlayerScreenClamper : ITickable
    {
        private Transform _playerTransform;
        private Rect _screenRect;

        [Inject]
        public void Construct(Player player)
        {
            _playerTransform = player.transform;
            _screenRect = new Rect(0, 0, Screen.width, Screen.height);
        }

        public void Tick() => Clamp();

        public void Clamp()
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(_playerTransform.position);

            if (_screenRect.Contains(screenPosition) == true)
                return;

            ReturnToScren(screenPosition);
        }

        private void ReturnToScren(Vector3 screenPosition)
        {
            float offset = 1;

            if (screenPosition.x < 0)
                _playerTransform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - offset, screenPosition.y, screenPosition.z));
            else if (screenPosition.x > Screen.width)
                _playerTransform.position = Camera.main.ScreenToWorldPoint(new Vector3(offset, screenPosition.y, screenPosition.z));

            if (screenPosition.y < 0)
                _playerTransform.position = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, Screen.height - offset, screenPosition.z));
            else if (screenPosition.y > Screen.height)
                _playerTransform.position = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, offset, screenPosition.z));
        }
    }
}