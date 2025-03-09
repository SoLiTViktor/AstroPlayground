using UnityEngine;

namespace AsteroidProject
{
    public abstract class Weapon : IWeapon
    {
        protected WeaponType WeaponType;

        protected Transform WeaponPositionPivot;

        public WeaponType Type => WeaponType;

        public void SetPositionPivot(Transform weaponPositionPivot)
        {
            WeaponPositionPivot = weaponPositionPivot;
        }

        public abstract void Shoot();
    }
}