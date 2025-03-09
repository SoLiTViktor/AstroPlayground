
namespace AsteroidProject
{
    public class BigAsteroid : Asteroid
    {
        public override void TakeDamage()
        {
            Explode();
        }

        protected override void SetType()
        {
            Type = EnemyType.BigAsteroid;
        }
    }
}