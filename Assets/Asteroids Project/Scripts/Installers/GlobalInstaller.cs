using UnityEngine;
using Zenject;

namespace AsteroidProject
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private GameCoreFileReader _gameCoreFileReader;
        [SerializeField] private TouchPadCanvasView _touchPadCanvas;

        public override void InstallBindings()
        {
            BindGameCoreData();
            BindGameCore();
            BindInput();
            BindPhysics();
            BindFactory();
        }

        private void BindGameCoreData()
        {
            GameCoreData gameCoreData = _gameCoreFileReader.LoadFromFile();

            Container
                .Bind<GameCoreData>()
                .FromInstance(gameCoreData)
                .AsSingle()
                .NonLazy();
        }

        private void BindGameCore()
        {
            Container
                .Bind<GameCore>()
                .AsSingle()
                .NonLazy();
        }

        private void BindInput()
        {

#if UNITY_EDITOR
#if UNITY_ANDROID
            BindTouchPadInput();
#else
            BindDesktopInput();
#endif
#else
            if (Application.platform == RuntimePlatform.WindowsPlayer)
                BindDesktopInput();
            else if (Application.platform == RuntimePlatform.Android)
                BindTouchPadInput();
#endif
        }

        private void BindPhysics()
        {
            Container
                .Bind<SimplifiedPhysics2D>()
                .AsSingle();
        }

        private void BindFactory()
        {
            Container
                .Bind<Factory>()
                .AsSingle()
                .NonLazy();
        }
        private void BindDesktopInput()
        {
            Container
                .BindInterfacesTo<DesktopInput>()
                .AsSingle();
        }

        private void BindTouchPadInput()
        {
            BindTouchPadCanvas();
            Container.Resolve<TouchPadCanvasView>().Enable();

            Container
                .BindInterfacesTo<TouchpadInput>()
                .AsSingle();
        }

        private void BindTouchPadCanvas()
        {
            Container
                .Bind<TouchPadCanvasView>()
                .FromInstance(_touchPadCanvas)
                .AsSingle();
        }
    }
}