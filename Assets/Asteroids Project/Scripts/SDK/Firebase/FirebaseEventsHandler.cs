using Firebase.Analytics;
using System;
using System.Collections.Generic;
using Zenject;

namespace AsteroidProject
{
    public class FirebaseEventsHandler : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        Dictionary<GameEventsType, string> _firebaseMessageMap;

        [Inject]
        private void Construct(SignalBus signalBus, Dictionary<GameEventsType, string> firebaseMessageMap)
        {
            _signalBus = signalBus;
            _firebaseMessageMap = firebaseMessageMap;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<DroidSelfExplodedSignal>(HandleDroidSelfExploding);
            _signalBus.Subscribe<EnemyCrushedSignal>(HandleDroidExploding);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<DroidSelfExplodedSignal>(HandleDroidSelfExploding);
            _signalBus.Unsubscribe<EnemyCrushedSignal>(HandleDroidExploding);
        }

        private void HandleDroidSelfExploding(DroidSelfExplodedSignal _)
        {
            FirebaseAnalytics.LogEvent(_firebaseMessageMap[GameEventsType.Droid_SelfExploded]);
        }

        private void HandleDroidExploding(EnemyCrushedSignal signalData)
        {
            if (signalData.Enemy.Type == EnemyType.Droid)
                FirebaseAnalytics.LogEvent(_firebaseMessageMap[GameEventsType.Droid_Exploded]);
        }
    }
}