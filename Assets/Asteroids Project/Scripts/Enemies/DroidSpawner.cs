using Cysharp.Threading.Tasks;
using System;
using Zenject;
using Random = UnityEngine.Random;

namespace AsteroidProject
{
    public class DroidSpawner : EnemySpawner
    {
        private GameObjectPool<Droid> _droids;

        private float _minDelay;
        private float _maxDelay;

        private IPlayer _playerTarget;

        [Inject]
        private void Construct(GameObjectPool<Droid> droids, IPlayer player, GameCore gameCore)
        {
            _droids = droids;
            _playerTarget = player;

            _minDelay = gameCore.GameCoreData.DroidSpawnerData.DroidMinDelaySpawn;
            _maxDelay = gameCore.GameCoreData.DroidSpawnerData.DroidMaxDelaySpawn;

            StartSpawn();
        }

        public override async void StartSpawn()
        {
            IsSpawning = true;
            await Spawn();
        }

        public override void StopSpawn()
        {
            IsSpawning = false;
            CancellationTokenSource.Cancel();
        }

        protected async UniTask Spawn()
        {
            System.Random random = new();
            float delay;

            while (IsSpawning)
            {
                Token.ThrowIfCancellationRequested();

                SpawnAreaRegardingScreen spawnArea = GenerateSpawnArea();

                Droid droid;

                if (_droids.TryGet(out droid))
                {
                    droid.transform.position = GenerateSpawnPosition(spawnArea);
                    droid.StartMoveToTarget(_playerTarget.Transform);
                    StartObjectScaling(droid);
                }

                delay = GenerateDelay();
                await UniTask.Delay(TimeSpan.FromSeconds(delay));
            }
        }

        private float GenerateDelay()
        {
            return Random.Range(_minDelay, _maxDelay);
        }
    }
}
