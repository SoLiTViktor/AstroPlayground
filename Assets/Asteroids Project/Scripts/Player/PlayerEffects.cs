namespace AsteroidProject
{
    public struct PlayerEffects
    {
        public PlayerEffects(IEffect shieldEffect, IEffect centralJetEffect, IEffect leftJetEffect, IEffect rightJetEffect)
        {
            ShieldEffect = shieldEffect;
            CentralJetEffect = centralJetEffect;
            LeftJetEffect = leftJetEffect;
            RightJetEffect = rightJetEffect;
        }

        public IEffect ShieldEffect { get; private set; }
        public IEffect CentralJetEffect { get; private set; }
        public IEffect LeftJetEffect { get; private set; }
        public IEffect RightJetEffect { get; private set; }
    }
}
