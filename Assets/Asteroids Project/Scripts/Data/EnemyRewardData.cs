using System;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidProject
{
    [CreateAssetMenu(fileName = "NewEnemyRewardData", menuName = "Objects Data/Enemy Reward Data")]
    public class EnemyRewardData : ScriptableObject
    {
        [Serializable]
        public class EnemyRewardItem
        {
            [SerializeField] private EnemyType _enemytype;
            [SerializeField] private int _reward;

            public EnemyType EnemyType => _enemytype;
            public int Reward => _reward;
        }

        [SerializeField] private List<EnemyRewardItem> _items;

        public List<EnemyRewardItem> GetFullItemListCopy() => new List<EnemyRewardItem>(_items);
    }
}
