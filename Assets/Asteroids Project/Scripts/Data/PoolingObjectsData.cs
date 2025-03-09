using System;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidProject
{
    [CreateAssetMenu(fileName = "NewPoolingObjectsData", menuName = "Pooling Objects Data")]
    public class PoolingObjectsData : ScriptableObject
    {
        [Serializable]
        public class PoolingObjectItem
        {
            [SerializeField] private PoolingObjectType _type;
            [SerializeField, SerializedInterface(typeof(IPoolable))] private GameObject _instantiatePrefab;
            [SerializeField, Min(1)] private int _amountToPool;

            public PoolingObjectType Type => _type;
            public GameObject Prefab => _instantiatePrefab;
            public int AmountToPool => _amountToPool;
        }

        [SerializeField] private PoolingObjectItem _wrongRequestItem;


        [SerializeField] private List<PoolingObjectItem> _poolingObjectItems;

        public List<PoolingObjectItem> GetFullListCopy() => new List<PoolingObjectItem>(_poolingObjectItems);
    }
}