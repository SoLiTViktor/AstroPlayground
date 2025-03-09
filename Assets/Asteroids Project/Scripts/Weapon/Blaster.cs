using Zenject;

namespace AsteroidProject
{
    public class Blaster : Weapon
    {
        private GameObjectPool<Bullet> _bulletsPool;

        [Inject]
        private void Construct(GameObjectPool<Bullet> pool)
        {
            _bulletsPool = pool;

            WeaponType = WeaponType.Blaster;
        }

        public override void Shoot()
        {
            Bullet bullet = _bulletsPool.Get();

            bullet.transform.position = WeaponPositionPivot.position;
            bullet.transform.rotation = WeaponPositionPivot.rotation;
        }
    }
}
