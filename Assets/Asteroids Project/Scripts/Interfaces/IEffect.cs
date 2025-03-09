using UnityEngine;

namespace AsteroidProject
{
    public interface IEffect
    {
        public Transform Transform { get; }
        public void Play();
        public void Stop();
    }
}