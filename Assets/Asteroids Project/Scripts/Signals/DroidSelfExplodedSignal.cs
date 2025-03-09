
namespace AsteroidProject
{
    public class DroidSelfExplodedSignal
    {
        public DroidSelfExplodedSignal(IEnemy droid)
        {
            Droid = droid;
        }

        public IEnemy Droid { get; private set; }
    }
}
