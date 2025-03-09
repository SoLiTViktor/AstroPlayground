using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AsteroidProject
{
    public class FirebaseInstaller : MonoInstaller
    {
        [SerializeField] private GameEventsData _gameEventsData;

        public override void InstallBindings()
        {
            CreateFirebaseSetup();
            BindEventsMessageMap();
            BindFirebaseEventHnadler();
        }

        private void CreateFirebaseSetup()
        {
            Container
                .Bind<FirebaseSetup>()
                .AsSingle()
                .NonLazy();
        }

        private void BindEventsMessageMap()
        {
            List<GameEventsData.GameEventsItem> items = _gameEventsData.GetFullItemListCopy();

            Dictionary<GameEventsType, string> firebaseMessageMap = new();

            foreach (var item in items)
                firebaseMessageMap.Add(item.GameEventType, item.Description);

            Container
                 .Bind<Dictionary<GameEventsType, string>>()
                 .FromInstance(firebaseMessageMap)
                 .AsSingle()
                 .NonLazy();
        }

        private void BindFirebaseEventHnadler()
        {
            Container
                .BindInterfacesAndSelfTo<FirebaseEventsHandler>()
                .AsSingle()
                .NonLazy();
        }
    }
}
