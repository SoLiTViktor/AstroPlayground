using UnityEngine;
using Zenject;

namespace AsteroidProject
{
    public abstract class Enemy : MonoBehaviour, IEnemy
    {
        protected SignalBus SignalBus;

        public GameObject GameObject => gameObject;
        public Transform Transform => transform;

        public EnemyType Type { get; protected set; } = EnemyType.None;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            SignalBus = signalBus;
        }

        protected void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayfieldBounds _))
                gameObject.SetActive(false);
            else if (collision.TryGetComponent(out IPlayer player))
                player.TakeDamage();
        }

        public abstract void TakeDamage();

        protected abstract void SetType();

        protected virtual void Explode()
        {
            SignalBus.Fire(new EnemyCrushedSignal(this));
            gameObject.SetActive(false);
        }
    }
}