using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace AsteroidProject
{
    public class Lazer : Ammo
    {
        private float _activateDuration;

        [Inject]
        private void Construct(GameCore gameCore)
        {
            _activateDuration = gameCore.GameCoreData.WeaponData.LazerActivateDuration;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IEnemy enemy))
            {
                enemy.TakeDamage();
            }
        }

        private async void OnEnable()
        {
            await Disable();
        }

        private async UniTask Disable()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_activateDuration));

            gameObject.SetActive(false);
            transform.SetParent(null);
        }
    }
}
