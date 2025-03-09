using System;
using UnityEngine;

namespace AsteroidProject
{
    public class SerializedInterfaceAttribute : PropertyAttribute
    {
        public Type MainType { get; }

        public SerializedInterfaceAttribute(Type type)
        {
            MainType = type;
        }
    }
}
