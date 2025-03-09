using System;
using UniRx;
using Zenject;

namespace AsteroidProject
{
    public class PlayerLives
    {
        private int _maxCount;

        public event Action Dead;
        public event Action Damaged;

        public readonly ReactiveProperty<int> Count = new();

        public bool IsDead => Count.Value == 0;

        [Inject]
        private void Construct(GameCore gameCore)
        {
            _maxCount = gameCore.GameCoreData.PlayerData.MaxLivesCount;
            Count.Value = _maxCount;
        }

        public void TakeDamage()
        {
            if (Count.Value == 0)
                return;

            --Count.Value;

            if (Count.Value > 0)
                Damaged?.Invoke();
            else
                Dead?.Invoke();
        }
    }
}
