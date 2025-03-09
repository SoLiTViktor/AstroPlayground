using UnityEngine;

namespace AsteroidProject
{
    [RequireComponent(typeof(ParticleSystem))]
    public class Effect : MonoBehaviour, IPoolable, IEffect
    {
        private ParticleSystem _effect;

        public GameObject GameObject => gameObject;
        public Transform Transform => transform;

        private void Awake() => _effect = GetComponent<ParticleSystem>();

        public void Play() => _effect.Play();

        public void Stop() => _effect.Stop();
    }
}
