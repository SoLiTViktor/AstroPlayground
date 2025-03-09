using System;

namespace AsteroidProject
{
    [Serializable]
    public struct WeaponData
    {
        public int LazerGunMaxAmmoCount;
        public float LazerGunRecharge;
        public float LazerActivateDuration;
        public int BulletSpeed;
    }
}