using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AsteroidProject
{
    public class BigAsteroidsSpawner : EnemySpawner
    {
        private Dictionary<PoolingObjectType, GameObjectPool<BigAsteroid>> _bigAsteroidPools;

        private float _spawnDuration;

        private int _min3dAxisTorque;
        private int _max3dAxisTorque;

        private float _minStartingPushForce;
        private float _maxStartingPushForce;

        [Inject]
        private void Construct(Dictionary<PoolingObjectType, GameObjectPool<BigAsteroid>> bigAsteroidsPool, GameCore gameCore)
        {
            _bigAsteroidPools = new(bigAsteroidsPool);

            FillAsteroidTypeMap();
            SetSpawnValues(gameCore);

            Token = CancellationTokenSource.Token;
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

        private void FillAsteroidTypeMap()
        {
            foreach (var pool in _bigAsteroidPools)
                SpawnedEnemyTypes.Add(pool.Key);
        }

        private void SetSpawnValues(GameCore gameCore)
        {
            _spawnDuration = gameCore.GameCoreData.BigAsteroidSpawnerData.AsteroidSpawnDuration;
            _min3dAxisTorque = gameCore.GameCoreData.BigAsteroidSpawnerData.Min3dAxisTorque;
            _max3dAxisTorque = gameCore.GameCoreData.BigAsteroidSpawnerData.Max3dAxisTorque;
            _minStartingPushForce = gameCore.GameCoreData.BigAsteroidSpawnerData.MinStartingPushForce;
            _maxStartingPushForce = gameCore.GameCoreData.BigAsteroidSpawnerData.MaxStartingPushForce;
        }

        private Vector2 GenerateMovingDirection(SpawnAreaRegardingScreen spawnArea)
        {
            System.Random random = new();

            Vector3 onScreenPosition = spawnArea switch
            {
                SpawnAreaRegardingScreen.Left => new Vector3(Screen.width, random.Next(0, Screen.height), 0),
                SpawnAreaRegardingScreen.Right => new Vector3(0, random.Next(0, Screen.height), 0),
                SpawnAreaRegardingScreen.Down => new Vector3(random.Next(0, Screen.width), Screen.height, 0),
                SpawnAreaRegardingScreen.Up => new Vector3(random.Next(0, Screen.width), 0, 0),
                _ => Vector3.zero,
            };

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(onScreenPosition);

            return new Vector2(worldPosition.x, worldPosition.y);
        }

        private Vector3 Generate3dTorque()
        {
            return new Vector3
                (
                UnityEngine.Random.Range(_min3dAxisTorque, _max3dAxisTorque),
                UnityEngine.Random.Range(_min3dAxisTorque, _max3dAxisTorque),
                UnityEngine.Random.Range(_min3dAxisTorque, _max3dAxisTorque)
                );
        }

        private float GeneratePushForce()
        {
            return UnityEngine.Random.Range(_minStartingPushForce, _maxStartingPushForce);
        }

        private async UniTask Spawn()
        {
            System.Random random = new();

            while (IsSpawning)
            {
                Token.ThrowIfCancellationRequested();

                PoolingObjectType nextEnemyType = SpawnedEnemyTypes[random.Next(SpawnedEnemyTypes.Count)];
                SpawnAreaRegardingScreen spawnArea = GenerateSpawnArea();

                BigAsteroid asteroid = _bigAsteroidPools[nextEnemyType].Get();
                asteroid.transform.position = GenerateSpawnPosition(spawnArea);
                asteroid.SetMovingDiraction(GenerateMovingDirection(spawnArea).normalized * GeneratePushForce());
                asteroid.Set3DRotation(Generate3dTorque());
                StartObjectScaling(asteroid);

                await UniTask.Delay(TimeSpan.FromSeconds(_spawnDuration), cancellationToken: Token);
            }
        }
    }
}
