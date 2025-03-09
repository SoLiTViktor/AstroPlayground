using Zenject;

namespace AsteroidProject
{
    public class SpawnersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindBigAsteroidSpawner();
            BindSmallAsteroidSpawner();
            BindDroidSpawner();
            BindExplosionsEffectSpawner();
        }

        private void BindBigAsteroidSpawner()
        {
            BigAsteroidsSpawner spawner = Container.Instantiate<BigAsteroidsSpawner>();

            Container
                .Bind<BigAsteroidsSpawner>()
                .FromInstance(spawner)
                .AsSingle();
        }

        private void BindSmallAsteroidSpawner()
        {
            SmallAsteroidsSpawner spawner = Container.Instantiate<SmallAsteroidsSpawner>();

            Container
                .Bind<SmallAsteroidsSpawner>()
                .FromInstance(spawner)
                .AsSingle();
        }


        private void BindDroidSpawner()
        {
            DroidSpawner spawner = Container.Instantiate<DroidSpawner>();

            Container
                .Bind<DroidSpawner>()
                .FromInstance(spawner)
                .AsSingle();
        }

        private void BindExplosionsEffectSpawner()
        {
            EffectSpawner  spawner = Container.Instantiate<EffectSpawner>();

            Container
                .BindInterfacesTo<EffectSpawner>()
                .FromInstance(spawner)
                .AsSingle();
        }
    }
}