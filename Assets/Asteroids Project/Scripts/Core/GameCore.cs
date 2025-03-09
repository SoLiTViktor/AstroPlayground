using Zenject;

namespace AsteroidProject
{
    public class GameCore
    {
        private GameCoreData _gameCoreData;
        public GameCoreData GameCoreData => _gameCoreData;

        [Inject]
        private void Construct(GameCoreData gameCoreData)
        {
            _gameCoreData = gameCoreData;
        }
    }
}
