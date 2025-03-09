using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AsteroidProject
{
    public class SmallAsteroidsSpawner : EnemySpawner
    {
        private SignalBus _signalBus;

        private Dictionary<PoolingObjectType, GameObjectPool<SmallAsteroid>> _smallAsteroidPools;

        private float _minMovingDirectionValue;
        private float _maxMovingDirectionValue;

        private int _min3dAxisTorque;
        private int _max3dAxisTorque;

        private float _minStartingPushForce;
        private float _maxStartingPushForce;

        private int _minAsteroidCreateCount;
        private int _maxAsteroidCreateCount;

        [Inject]
        private void Construct(Dictionary<PoolingObjectType, GameObjectPool<SmallAsteroid>> smallAsteroidsPool, SignalBus signalBus, GameCore gameCore)
        {
            _signalBus = signalBus;
            _smallAsteroidPools = new(smallAsteroidsPool);

            FillAsteroidTypeMap();
            SetSpawnValues(gameCore);
            StartSpawn();
        }

        public override void StartSpawn()
        {
            _signalBus.Subscribe<EnemyCrushedSignal>(HandleExplode);
        }

        public override void StopSpawn()
        {
            _signalBus.Unsubscribe<EnemyCrushedSignal>(HandleExplode);
        }

        private void FillAsteroidTypeMap()
        {
            foreach (var pool in _smallAsteroidPools)
                SpawnedEnemyTypes.Add(pool.Key);
        }

        private void SetSpawnValues(GameCore gameCore)
        {
            _minAsteroidCreateCount = gameCore.GameCoreData.SmallAsteroidSpawnerData.MinAsteroidCreateCount;
            _maxAsteroidCreateCount = gameCore.GameCoreData.SmallAsteroidSpawnerData.MaxAsteroidCreateCount;

            _minStartingPushForce = gameCore.GameCoreData.SmallAsteroidSpawnerData.MinStartingPushForce;
            _maxStartingPushForce = gameCore.GameCoreData.SmallAsteroidSpawnerData.MaxStartingPushForce;

            _minMovingDirectionValue = gameCore.GameCoreData.SmallAsteroidSpawnerData.MinMovingDirectionValue;
            _maxMovingDirectionValue = gameCore.GameCoreData.SmallAsteroidSpawnerData.MaxMovingDirectionValue;

            _min3dAxisTorque = gameCore.GameCoreData.SmallAsteroidSpawnerData.Min3dAxisTorque;
            _max3dAxisTorque = gameCore.GameCoreData.SmallAsteroidSpawnerData.Max3dAxisTorque;
        }

        private void HandleExplode(EnemyCrushedSignal signalData)
        {
            if (signalData.Enemy is BigAsteroid)
                Spawn(signalData.Enemy.Transform.position);
        }

        private Vector2 GenerateMovingDirection(Vector3 startPosition)
        {
            Vector2 randomPoint = new Vector2
                (
                Random.Range(_minMovingDirectionValue, _maxMovingDirectionValue),
                Random.Range(_minMovingDirectionValue, _maxMovingDirectionValue)
                );

            return randomPoint - new Vector2(startPosition.x, startPosition.y);
        }

        private Vector3 Generate3dTorque()
        {
            return new Vector3
                (
                Random.Range(_min3dAxisTorque, _max3dAxisTorque), 
                Random.Range(_min3dAxisTorque, _max3dAxisTorque), 
                Random.Range(_min3dAxisTorque, _max3dAxisTorque)
                );
        }

        private float GeneratePushForce()
        {
            return Random.Range(_minStartingPushForce, _maxStartingPushForce);
        }

        private int GenerateAsteroidCount()
        {
            System.Random random = new();

            return random.Next(_minAsteroidCreateCount, _maxAsteroidCreateCount);
        }

        private void Spawn(Vector3 spawnPosition)
        {
            System.Random random = new();

            int asteroidCount = GenerateAsteroidCount();

            for (int i = 0; i < asteroidCount; i++)
            {
                PoolingObjectType nextEnemyType = SpawnedEnemyTypes[random.Next(SpawnedEnemyTypes.Count)];
                SmallAsteroid asteroid = _smallAsteroidPools[nextEnemyType].Get();
                asteroid.transform.position = spawnPosition;

                Vector2 direction = GenerateMovingDirection(spawnPosition);
                asteroid.SetMovingDiraction(direction.normalized * GeneratePushForce());
                asteroid.Set3DRotation(Generate3dTorque());
            }
        }
    }
}