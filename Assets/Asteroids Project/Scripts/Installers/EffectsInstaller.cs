using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AsteroidProject
{
    public class EffectsInstaller : MonoInstaller
    {
        [SerializeField] private EffectData _effectData;

        public override void InstallBindings()
        {
            BindEffectMap();
            BindPlayerEffect();
        }

        private void BindEffectMap()
        {
            List<EffectData.EffectItem> effetcsItem = _effectData.GetFullItemListCopy();
            Dictionary<EffectType, Effect> effects = new Dictionary<EffectType, Effect>();

            foreach (EffectData.EffectItem item in effetcsItem)
                if (item.Effect.TryGetComponent(out Effect effect))
                    effects.Add(item.EffectType, Instantiate(effect));

            Container
                .Bind<Dictionary<EffectType, Effect>>()
                .FromInstance(effects)
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerEffect()
        {
            Dictionary<EffectType, Effect> effectMap = Container.Resolve<Dictionary<EffectType, Effect>>();

            Container
                .Bind<PlayerEffects>()
                .AsSingle()
                .WithArguments
                (
                    effectMap[EffectType.Shield],
                    effectMap[EffectType.Jet],
                    Instantiate(effectMap[EffectType.Jet]),
                    Instantiate(effectMap[EffectType.Jet])
                );
        }
    }
}
