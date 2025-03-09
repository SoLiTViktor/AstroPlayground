using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AsteroidProject
{
    public class GameObjectPool<T> where T : class, IPoolable
    {
        private List<T> _objects;

        private Dictionary<T, float> _objectsLifetime;

        public GameObjectPool(List<T> objects)
        {
            _objects = new List<T>(objects);

            foreach (T obj in _objects)
                obj.GameObject.SetActive(false);

            _objectsLifetime = new Dictionary<T, float>();
        }

        public T Get()
        {
            T nextObject = _objects.FirstOrDefault(obj => obj.GameObject.activeSelf == false);

            if (nextObject == null)
                nextObject = DisableOldestObject();

            nextObject.GameObject.SetActive(true);

            if (_objectsLifetime.ContainsKey(nextObject))
                _objectsLifetime[nextObject] = Time.time;
            else
                _objectsLifetime.Add(nextObject, Time.time);

            return nextObject;
        }

        public bool TryGet(out T nextObject)
        {
            nextObject = _objects.FirstOrDefault(obj => obj.GameObject.activeSelf == false);

            if (nextObject != null)
            {
                nextObject.GameObject.SetActive(true);

                if (_objectsLifetime.ContainsKey(nextObject))
                    _objectsLifetime[nextObject] = Time.time;
                else
                    _objectsLifetime.Add(nextObject, Time.time);
            }

            return nextObject != null;
        }

        private T DisableOldestObject()
        {
            T oldestObject = _objectsLifetime.Aggregate((left, right) => left.Value < right.Value ? left : right).Key;
            oldestObject.GameObject.SetActive(false);
            return oldestObject;
        }
    }
}
