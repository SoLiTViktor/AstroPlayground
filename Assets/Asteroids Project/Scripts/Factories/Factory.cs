using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AsteroidProject
{
    public class Factory
    {
        private DiContainer _container;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }

        public GameObjectPool<T> CreatePool<T>(T prefab, int poolCount) where T : MonoBehaviour, IPoolable
        {
            List<T> _objects = new List<T>();
            T newObject;

            for (int i = 0; i < poolCount; i++)
            {
                newObject = _container.InstantiatePrefabForComponent<T>(prefab);
                _objects.Add(newObject);
            }

            return new GameObjectPool<T>(_objects);
        }
    }
}