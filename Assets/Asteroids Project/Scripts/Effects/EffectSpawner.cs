using System;
using System.Collections.Generic;
using Zenject;
using Vector3 = UnityEngine.Vector3;

namespace AsteroidProject
{
    public class EffectSpawner : IInitializable, IDisposable
    {
        private Dictionary<PoolingObjectType, GameObjectPool<Effect>> _effectsMap;
        private Dictionary<EffectType, Effect> _singleEffects;

        SignalBus _signalBus;

        [Inject]
        private void Construct(
            Dictionary<PoolingObjectType, GameObjectPool<Effect>> effects, Dictionary<EffectType, Effect> singleEffects, SignalBus signalBus)
        {
            _effectsMap = effects;
            _singleEffects = singleEffects;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<EnemyCrushedSignal>(SpawnEnemyExplode);
            _signalBus.Subscribe<DroidSelfExplodedSignal>(SpawnSelfExplodeEvent);
            _signalBus.Subscribe<PlayerCrushedSignal>(SpawnPlayerExplosion);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<EnemyCrushedSignal>(SpawnEnemyExplode);
            _signalBus.Unsubscribe<DroidSelfExplodedSignal>(SpawnSelfExplodeEvent);
            _signalBus.Unsubscribe<PlayerCrushedSignal>(SpawnPlayerExplosion);
        }

        private void SpawnPlayerExplosion(PlayerCrushedSignal signalData)
        {
            Effect effect = _singleEffects[EffectType.PlayerExpode];
            ShowEffect(effect, signalData.Player.Transform.position);
        }

        private void SpawnSelfExplodeEvent(DroidSelfExplodedSignal signalData)
        {
            Effect effect = _effectsMap[PoolingObjectType.Effect_BigExplode].Get();
            ShowEffect(effect, signalData.Droid.Transform.position);
        }

        private void SpawnEnemyExplode(EnemyCrushedSignal signalData)
        {
            Effect effect;

            if (signalData.Enemy.Type == EnemyType.SmallAsteroid)
                effect = _effectsMap[PoolingObjectType.Effect_SmallExplode].Get();
            else
                effect = _effectsMap[PoolingObjectType.Effect_BigExplode].Get();

            ShowEffect(effect, signalData.Enemy.Transform.position);
        }

        private void ShowEffect(Effect effect, Vector3 position)
        {
            effect.transform.position = position;
            effect.Play();
        }
    }
}
