using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AsteroidProject
{
    public class PlayerEffectViewer
    {
        private IEffect _shieldEffect;
        private IEffect _centralJetEffect;
        private IEffect _leftJetEffect;
        private IEffect _rightJetEffect;

        [Inject]
        private void Construct(PlayerEffects playerEffects)
        {
            _shieldEffect = playerEffects.ShieldEffect;
            _centralJetEffect = playerEffects.CentralJetEffect;
            _leftJetEffect = playerEffects.LeftJetEffect;
            _rightJetEffect = playerEffects.RightJetEffect;
        }

        public void SetEffectPosition(Transform player, Transform centralJet, Transform leftJet, Transform rightJet)
        {
            _shieldEffect.Transform.position = player.position;
            _shieldEffect.Transform.SetParent(player);

            _centralJetEffect.Transform.position = centralJet.position;
            _centralJetEffect.Transform.SetParent(centralJet);

            _leftJetEffect.Transform.position = leftJet.position;
            _leftJetEffect.Transform.SetParent(leftJet);

            _rightJetEffect.Transform.position = rightJet.position;
            _rightJetEffect.Transform.SetParent(rightJet);
        }

        public void ActivateShield()
        {
            _shieldEffect.Play();
        }

        public void PlayMovingEffects(Vector2 axis)
        {
            if (axis.x > 0)
                _leftJetEffect.Play();
            else if (axis.x < 0)
                _rightJetEffect.Play();
            else
                StopRotationEffect();

            if (axis.y > 0)
                _centralJetEffect.Play();
            else
                _centralJetEffect.Stop();
        }
        public void StopMovingEffects()
        {
            StopRotationEffect();
            _centralJetEffect.Stop();
        }

        private void StopRotationEffect()
        {
            _leftJetEffect.Stop();
            _rightJetEffect.Stop();
        }
    }
}
