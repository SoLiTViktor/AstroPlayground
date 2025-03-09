using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace AsteroidProject
{
    public class PlayerWeaponry
    {
        private Transform _weaponPivot;

        private IWeapon _blaster;
        private IWeapon _lazerGun;

        [Inject]
        private void Construct(List<IWeapon> weapons)
        {
            _blaster = weapons.Where(weapon => weapon.Type == WeaponType.Blaster).FirstOrDefault();
            _lazerGun = weapons.Where(weapon => weapon.Type == WeaponType.LazerGun).FirstOrDefault();
        }

        public void SetWeaponPosiotion(Transform weaponPivot)
        {
            _weaponPivot = weaponPivot;
            _blaster.SetPositionPivot(_weaponPivot);
            _lazerGun.SetPositionPivot(_weaponPivot);
        }

        public void BasicShoot()
        {
            _blaster.Shoot();
        }

        public void MightShoot()
        {
            _lazerGun.Shoot();
        }
    }
}
