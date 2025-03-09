using UnityEngine;

namespace AsteroidProject
{
    public interface IPlayer : IDamageable
    {
        public Transform Transform { get; }
    }
}
