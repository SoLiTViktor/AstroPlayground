using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using static AsteroidProject.PoolingObjectsData;

namespace AsteroidProject
{
    public class WeaponInstaller : MonoInstaller
    {
        [SerializeField] private PoolingObjectsData _weaponsData;

        private List<PoolingObjectItem> _weaponItems;

        public override void InstallBindings()
        {
            _weaponItems = _weaponsData.GetFullListCopy();

            BindBulltsPool();
            BindLazerPool();

            BindBlaster();
            BindLazerGun();
            BindWeaponList();

        }

        private void BindBulltsPool()
        {
            PoolingObjectItem bulletItem = _weaponItems.FirstOrDefault(item => item.Type == PoolingObjectType.Weapon_Bullet);
            GameObjectPool<Bullet> bulletPool;

            if (bulletItem.Prefab.TryGetComponent(out Bullet prefab))
            {
                Factory factory = Container.Resolve<Factory>();
                bulletPool = factory.CreatePool(prefab, bulletItem.AmountToPool);

                Container
                    .Bind<GameObjectPool<Bullet>>()
                    .FromInstance(bulletPool)
                    .AsSingle();
            }
        }

        private void BindLazerPool()
        {
            PoolingObjectItem lazerItem = _weaponItems.FirstOrDefault(item => item.Type == PoolingObjectType.Weapon_Lazer);
            GameObjectPool<Lazer> lazerPool;

            if (lazerItem.Prefab.TryGetComponent(out Lazer prefab))
            {
                Factory factory = Container.Resolve<Factory>();
                lazerPool = factory.CreatePool(prefab, lazerItem.AmountToPool);

                Container
                    .Bind<GameObjectPool<Lazer>>()
                    .FromInstance(lazerPool)
                    .AsSingle();
            }
        }

        private void BindBlaster()
        {
            Container.Instantiate<Blaster>();

            Container
                .BindInterfacesAndSelfTo<Blaster>()
                .AsSingle();
        }

        private void BindLazerGun()
        {
            Container.Instantiate<LazerGun>();

            Container
                .BindInterfacesAndSelfTo<LazerGun>()
                .AsSingle();
        }

        private void BindWeaponList()
        {
            List<IWeapon> weapons = new List<IWeapon>
            {
                Container.Resolve<Blaster>(),
                Container.Resolve<LazerGun>(),
            };

            Container
                .Bind<List<IWeapon>>()
                .FromInstance(weapons)
                .AsSingle();
        }
    }
}
