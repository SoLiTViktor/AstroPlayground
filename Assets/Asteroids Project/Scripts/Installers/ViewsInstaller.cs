using UnityEngine;
using Zenject;

namespace AsteroidProject
{
    public class ViewsInstaller : MonoInstaller
    {
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private PlayerDataView _playerDataView;
        [SerializeField] private LazerDataView _lazerDataView;
        [SerializeField] private LivesView _livesView;

        public override void InstallBindings()
        {
            BindScoreView();
            BindScoreViewModel();

            BindPlayerDataView();
            BindPlayerDataViewModel();

            BindLazerDataView();
            BindLazerDataViewModel();

            BindPlayerLivesView();
            BindPlayerLivesViewModel();
        }

        private void BindScoreView()
        {
            Container
                .Bind<ScoreView>()
                .FromInstance(_scoreView)
                .AsSingle()
                .NonLazy();
        }

        private void BindScoreViewModel()
        {
            ScoreViewModel viewModel = Container.Instantiate<ScoreViewModel>();

            Container
                .Bind<ScoreViewModel>()
                .FromInstance(viewModel)
                .AsSingle();
        }

        private void BindPlayerDataView()
        {
            Container
                .Bind<PlayerDataView>()
                .FromInstance(_playerDataView)
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerDataViewModel()
        {
            PlayerDataViewModel viewModel = Container.Instantiate<PlayerDataViewModel>();

            Container
                .Bind<PlayerDataViewModel>()
                .FromInstance(viewModel)
                .AsSingle();
        }

        private void BindLazerDataView()
        {
            Container
                .Bind<LazerDataView>()
                .FromInstance(_lazerDataView)
                .AsSingle()
                .NonLazy();
        }

        private void BindLazerDataViewModel()
        {
            LazerDataViewModel viewModel = Container.Instantiate<LazerDataViewModel>();

            Container
                .Bind<LazerDataViewModel>()
                .FromInstance(viewModel)
                .AsSingle();
        }

        private void BindPlayerLivesView()
        {
            Container
                .Bind<LivesView>()
                .FromInstance(_livesView)
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerLivesViewModel()
        {
            LivesViewModel viewModel = Container.Instantiate<LivesViewModel>();

            Container
                .Bind<LivesViewModel>()
                .FromInstance(viewModel)
                .AsSingle();
        }
    }
}

