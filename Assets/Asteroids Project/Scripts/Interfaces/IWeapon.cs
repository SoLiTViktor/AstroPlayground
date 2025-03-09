using UnityEngine;

namespace AsteroidProject
{
    public interface IWeapon
    {
        public WeaponType Type { get; }

        public void SetPositionPivot(Transform positionPivot);

        public void Shoot();
    }
}
