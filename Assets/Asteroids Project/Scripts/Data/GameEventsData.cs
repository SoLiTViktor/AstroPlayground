using AsteroidProject;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameEventsData", menuName = "Objects Data/Game Events Data")]
public class GameEventsData : ScriptableObject
{
    [Serializable]
    public class GameEventsItem
    {
        [SerializeField] private GameEventsType _gameEventType;
        [SerializeField] private string _description;

        public GameEventsType GameEventType => _gameEventType;
        public string Description => _description;
    }

    [SerializeField] private List<GameEventsItem> _items;

    public List<GameEventsItem> GetFullItemListCopy() => new List<GameEventsItem>(_items);
}
