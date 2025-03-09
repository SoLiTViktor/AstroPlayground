using Zenject;

public class ApplovinInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindInterstitialAdd();
    }

    private void BindInterstitialAdd()
    {
        Container
            .BindInterfacesAndSelfTo<Interstitial>()
            .AsSingle()
            .NonLazy();
    }
}
