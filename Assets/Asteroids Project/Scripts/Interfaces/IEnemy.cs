using UnityEngine;

namespace AsteroidProject
{
    public interface IEnemy : IDamageable
    { 
        public EnemyType Type { get; }

        public Transform Transform { get; }
    }
}
