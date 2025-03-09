using UnityEngine;
using Zenject;

namespace AsteroidProject
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Transform _playerStartPoint;
        [SerializeField] private Player _playerPrefab;

        [Space(15)]
        [SerializeField] private EnemyRewardData _enemyRewardData;

        public override void InstallBindings()
        {
            BindLives();
            BindPlayerWeaponry();
            BindPlayerEffectsViewer();
            BindPlayer();
            BindPlayerMovement();
            BindPlayerScreenClamper();
            BindEnemyRewardData();
            BindPlayerScoreCounter();
        }

        private void BindLives()
        {
            Container
                .Bind<PlayerLives>()
                .AsSingle();
        }

        private void BindPlayerWeaponry()
        {
            Container
                .Bind<PlayerWeaponry>()
                .AsSingle();
        }

        private void BindPlayerEffectsViewer()
        {
            Container
                .Bind<PlayerEffectViewer>()
                .AsSingle();
        }

        private void BindPlayer()
        {
            Player player = Container
                .InstantiatePrefabForComponent<Player>(_playerPrefab, _playerStartPoint.position, Quaternion.identity, null);

            Container
                .BindInterfacesAndSelfTo<Player>()
                .FromInstance(player)
                .AsSingle();
        }

        private void BindPlayerMovement()
        {
            PlayerMovement playerMovement = Container.Resolve<Player>().GetComponent<PlayerMovement>();

            Container
                .Bind<PlayerMovement>()
                .FromInstance(playerMovement)
                .AsSingle();
        }

        private void BindPlayerScreenClamper()
        {
            Container.BindInterfacesTo<PlayerScreenClamper>().AsSingle();
        }

        private void BindEnemyRewardData()
        {
            Container
                .Bind<EnemyRewardData>()
                .FromInstance(_enemyRewardData)
                .AsSingle();
        }

        private void BindPlayerScoreCounter()
        {
            Container
                .BindInterfacesAndSelfTo<ScoreCounter>()
                .AsSingle();
        }
    }
}