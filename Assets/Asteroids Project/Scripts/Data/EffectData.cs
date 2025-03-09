using System;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidProject
{
    [CreateAssetMenu(fileName = "NewEffectData", menuName = "Objects Data/Effect Data")]
    public class EffectData : ScriptableObject
    {
        [Serializable]
        public class EffectItem
        {
            [SerializeField] private EffectType _type;
            [SerializeField, SerializedInterface(typeof(IEffect))] private GameObject _effect;

            public EffectType EffectType => _type;
            public GameObject Effect => _effect;
        }

        [SerializeField] private List<EffectItem> _items;

        public List<EffectItem> GetFullItemListCopy() => new List<EffectItem>(_items);
    }
}
