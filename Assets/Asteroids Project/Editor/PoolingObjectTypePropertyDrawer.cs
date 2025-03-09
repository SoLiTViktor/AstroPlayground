using UnityEditor;

namespace AsteroidProject
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(PoolingObjectType))]
    public sealed class PoolingObjectTypePropertyDrawer : EnumPropertyDrawer<PoolingObjectType>
    {
    }
#endif
}