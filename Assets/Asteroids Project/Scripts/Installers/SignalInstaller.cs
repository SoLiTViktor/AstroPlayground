using Zenject;

namespace AsteroidProject
{
    public class SignalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSignalBus();
            DeclareSignals();
        }

        private void BindSignalBus()
        {
            Container
                .Bind<SignalBusInstaller>()
                .AsSingle();
            
            Container.Resolve<SignalBusInstaller>().InstallBindings();
        }
        private void DeclareSignals()
        {
            Container.DeclareSignal<PlayerCrushedSignal>();
            Container.DeclareSignal<EnemyCrushedSignal>();
            Container.DeclareSignal<DroidSelfExplodedSignal>();
        }
    }
}
