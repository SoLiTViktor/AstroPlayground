using System;

namespace AsteroidProject
{
    [Serializable]
    public class GameCoreData
    {
        public PlayerData PlayerData;
        public WeaponData WeaponData;
        public EnemySpawnerData EnemySpawnerData;
        public BigAsteroidSpawnerData BigAsteroidSpawnerData;
        public SmallAsteroidSpawnerData SmallAsteroidSpawnerData;
        public DroidSpawnerData DroidSpawnerData;

        public DroidData DroidData;
        public AsteroidStruct[] AsteroidData;

        public float PlayfieldOffset;
    }
}
