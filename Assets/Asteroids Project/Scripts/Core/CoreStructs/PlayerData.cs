using System;

namespace AsteroidProject
{
    [Serializable]
    public struct PlayerData
    {
        public int MaxLivesCount;
        public float MovingSpeed;
        public float RotationSpeed;
        public float StunDuration;
        public float ShieldDuration;
    }
}
