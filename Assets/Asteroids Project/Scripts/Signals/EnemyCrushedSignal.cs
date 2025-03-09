
namespace AsteroidProject
{
    public class EnemyCrushedSignal
    {
        public EnemyCrushedSignal(IEnemy enemy)
        {
            Enemy = enemy;
        }

        public IEnemy Enemy { get; private set; }
    }
}
