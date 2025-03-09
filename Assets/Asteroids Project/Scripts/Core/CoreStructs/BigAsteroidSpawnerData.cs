using System;

namespace AsteroidProject
{
    [Serializable]
    public struct BigAsteroidSpawnerData
    {
        public float AsteroidSpawnDuration;
        public int Min3dAxisTorque;
        public int Max3dAxisTorque;
        public float MinStartingPushForce;
        public float MaxStartingPushForce;
    }
}
