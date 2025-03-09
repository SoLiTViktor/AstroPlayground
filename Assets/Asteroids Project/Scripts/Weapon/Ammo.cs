using UnityEngine;

namespace AsteroidProject
{
    public abstract class Ammo : MonoBehaviour, IPoolable
    {
        public GameObject GameObject => gameObject;

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayfieldBounds _bounds))
                gameObject.SetActive(false);
        }
    }
}
