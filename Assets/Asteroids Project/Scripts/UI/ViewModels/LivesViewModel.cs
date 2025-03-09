using UnityEngine;
using UniRx;
using Zenject;

namespace AsteroidProject
{
    public class LivesViewModel
    {
        private PlayerLives _playerLives;
        private LivesView _livesView;

        [Inject]
        private void Construct(PlayerLives playerLives, LivesView livesView)
        {
            _playerLives = playerLives;
            _livesView = livesView;

            _playerLives.Count.Subscribe(RedrawScore);
        }

        private void RedrawScore(int lives)
        {
            _livesView.ChangeLivesText(lives.ToString());
        }
    }
}
