
namespace AsteroidProject
{
    public class SmallAsteroid : Asteroid
    {
        public override void TakeDamage()
        {
            Explode();
        }

        protected override void SetType()
        {
            Type = EnemyType.SmallAsteroid;
        }
    }
}