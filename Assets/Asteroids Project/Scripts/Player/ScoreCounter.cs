using System;
using System.Collections.Generic;
using UniRx;
using Zenject;
using static AsteroidProject.EnemyRewardData;

namespace AsteroidProject
{
    public class ScoreCounter : IInitializable, IDisposable
    {
        private Dictionary<EnemyType, int> _enemyReward;
        private SignalBus _signalBus;

        public readonly ReactiveProperty<int> Score = new();

        [Inject]
        private void Construct(SignalBus signalBus, EnemyRewardData enemyRewardData)
        {
            _enemyReward = new();
            _signalBus = signalBus;

            FillEnemyReward(enemyRewardData);

            Score.Value = 0;
        }

        public void FillEnemyReward(EnemyRewardData enemyRewardData)
        {
            _enemyReward = new();

            List<EnemyRewardItem> items = enemyRewardData.GetFullItemListCopy();

            foreach (EnemyRewardItem item in items)
                _enemyReward.Add(item.EnemyType, item.Reward);
        }

        public void Initialize() => _signalBus.Subscribe<EnemyCrushedSignal>(IdentifyEnemy);

        public void Dispose() => _signalBus.Unsubscribe<EnemyCrushedSignal>(IdentifyEnemy);

        private void IdentifyEnemy(EnemyCrushedSignal signalData)
        {
            if (_enemyReward.ContainsKey(signalData.Enemy.Type))
                ChangeScore(_enemyReward[signalData.Enemy.Type]);
        }

        private void ChangeScore(int points)
        {
            Score.Value += points;
        }
    }
}