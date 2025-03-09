using System;

namespace AsteroidProject
{
    [Serializable]
    public struct SmallAsteroidSpawnerData
    {
        public int MinAsteroidCreateCount;
        public int MaxAsteroidCreateCount;
        public float MinMovingDirectionValue;   
        public float MaxMovingDirectionValue;
        public float MinStartingPushForce;
        public float MaxStartingPushForce;
        public int Min3dAxisTorque;
        public int Max3dAxisTorque;
    }
}
