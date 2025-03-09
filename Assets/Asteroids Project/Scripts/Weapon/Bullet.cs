using UnityEngine;
using Zenject;

namespace AsteroidProject
{
    public class Bullet : Ammo
    {
        private float _speed;

        [Inject]
        private void Construct(GameCore gameCore)
        {
            _speed = gameCore.GameCoreData.WeaponData.BulletSpeed;
        }

        private void Update() => transform.position += transform.up * _speed * Time.fixedDeltaTime;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IEnemy enemy))
            {
                enemy.TakeDamage();
                gameObject.SetActive(false);
            }
        }
    }
}