using System;

namespace AsteroidProject
{
    [Serializable]
    public struct DroidData
    {
        public float MoveSpeed;
        public float RotateSpeed;
        public float ModelTorque;
        public int SelfExplodeDelayMilliseconds;
    }
}