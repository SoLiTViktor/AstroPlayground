using System.Collections.Generic;
using System;
using UnityEngine;
using Random = System.Random;
using System.Threading;
using DG.Tweening;
using Zenject;

namespace AsteroidProject
{
    public abstract class EnemySpawner
    {
        protected List<PoolingObjectType> SpawnedEnemyTypes = new();

        protected CancellationTokenSource CancellationTokenSource = new();

        protected CancellationToken Token;

        private float _screenOffset;

        private float _scaleInEnable;
        private float _scaledDuration;

        protected bool IsSpawning;

        protected enum SpawnAreaRegardingScreen
        {
            Left,
            Right,
            Up,
            Down
        }

        [Inject]
        private void Construct(GameCore gameCore)
        {
            _screenOffset = gameCore.GameCoreData.EnemySpawnerData.ScreenOffset;
            _scaleInEnable = gameCore.GameCoreData.EnemySpawnerData.ScaleInEnable;
            _scaledDuration = gameCore.GameCoreData.EnemySpawnerData.ScaledDuration;
        }

        public abstract void StartSpawn();

        public abstract void StopSpawn();

        protected SpawnAreaRegardingScreen GenerateSpawnArea()
        {
            Random random = new Random();
            Type type = typeof(SpawnAreaRegardingScreen);
            Array values = type.GetEnumValues();
            int index = random.Next(values.Length);

            return (SpawnAreaRegardingScreen)values.GetValue(index);
        }

        protected Vector3 GenerateSpawnPosition(SpawnAreaRegardingScreen spawnArea)
        {
            Random random = new Random();

            Vector3 onScreenPosition = spawnArea switch
            {
                SpawnAreaRegardingScreen.Left => new Vector3(-_screenOffset, random.Next(0, Screen.height), 0),
                SpawnAreaRegardingScreen.Right => new Vector3(Screen.width + _screenOffset, random.Next(0, Screen.height), 0),
                SpawnAreaRegardingScreen.Down => new Vector3(random.Next(0, Screen.width), -_screenOffset, 0),
                SpawnAreaRegardingScreen.Up => new Vector3(random.Next(0, Screen.width), Screen.height + _screenOffset, 0),
                _ => Vector3.zero,
            };

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(onScreenPosition);

            return new Vector3(worldPosition.x, worldPosition.y, 0);
        }

        protected void StartObjectScaling(Enemy enemy)
        {
            Vector3 scale = enemy.transform.localScale;

            enemy.transform.localScale *= _scaleInEnable;
            enemy.transform.DOScale(scale, _scaledDuration).SetEase(Ease.OutExpo);
        }
    }
}
