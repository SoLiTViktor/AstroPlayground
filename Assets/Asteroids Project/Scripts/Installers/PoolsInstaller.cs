using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using static AsteroidProject.PoolingObjectsData;

namespace AsteroidProject
{
    public class PoolsInstaller : MonoInstaller
    {
        [SerializeField] private PoolingObjectsData _enemyPoolData;
        [SerializeField] private PoolingObjectsData _effectPoolData;

        private List<PoolingObjectItem> _enemyItems;
        private List<PoolingObjectItem> _effectItems;

        public override void InstallBindings()
        {
            _enemyItems = _enemyPoolData.GetFullListCopy();
            _effectItems = _effectPoolData.GetFullListCopy();

            BindBigAsteroidsPoolMap();
            BindSmallAsteroidsPoolMap();
            BindDroidsPool();
            BindExplodeEffectPool();
        }

        private void BindBigAsteroidsPoolMap()
        {
            Dictionary<PoolingObjectType, GameObjectPool<BigAsteroid>> bigAsteroidsPoolMap = GetNewPoolMap<BigAsteroid>(_enemyItems);

            Container
                .Bind<Dictionary<PoolingObjectType, GameObjectPool<BigAsteroid>>>()
                .FromInstance(bigAsteroidsPoolMap)
                .AsSingle();
        }

        private void BindSmallAsteroidsPoolMap()
        {
            Dictionary<PoolingObjectType, GameObjectPool<SmallAsteroid>> smallAsteroidsPoolMap = GetNewPoolMap<SmallAsteroid>(_enemyItems);

            Container
                .Bind<Dictionary<PoolingObjectType, GameObjectPool<SmallAsteroid>>>()
                .FromInstance(smallAsteroidsPoolMap)
                .AsSingle();
        }

        private void BindDroidsPool()
        {
            PoolingObjectItem droidItem = _enemyItems.FirstOrDefault(item => item.Type == PoolingObjectType.Enemy_Droid);
            GameObjectPool<Droid> droidPool;

            if (droidItem.Prefab.TryGetComponent(out Droid prefab))
            {
                Factory factory = Container.Resolve<Factory>();
                droidPool = factory.CreatePool(prefab, droidItem.AmountToPool);

                Container
                    .Bind<GameObjectPool<Droid>>()
                    .FromInstance(droidPool)
                    .AsSingle();
            }
        }

        private void BindExplodeEffectPool()
        {
            Dictionary<PoolingObjectType, GameObjectPool<Effect>> effectPoolMap = GetNewPoolMap<Effect>(_effectItems);

            Container
                .Bind<Dictionary<PoolingObjectType, GameObjectPool<Effect>>>()
                .FromInstance(effectPoolMap)
                .AsSingle();
        }

        private Dictionary<PoolingObjectType, GameObjectPool<T>> GetNewPoolMap<T>(List<PoolingObjectItem> items) where T : MonoBehaviour, IPoolable
        {
            Factory factory = Container.Resolve<Factory>();
            Dictionary<PoolingObjectType, GameObjectPool<T>> newPoolMap = new();

            foreach (PoolingObjectItem poolingObjectItem in items)
                if (poolingObjectItem.Prefab.TryGetComponent(out T prefab))
                    newPoolMap.Add(poolingObjectItem.Type, factory.CreatePool(prefab, poolingObjectItem.AmountToPool));

            return newPoolMap;
        }
    }
}
