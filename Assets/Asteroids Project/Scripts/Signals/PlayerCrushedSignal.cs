namespace AsteroidProject
{
    public class PlayerCrushedSignal
    {
        public PlayerCrushedSignal(IPlayer player)
        {
            Player = player;
        }

        public IPlayer Player { get; private set; }
    }
}
