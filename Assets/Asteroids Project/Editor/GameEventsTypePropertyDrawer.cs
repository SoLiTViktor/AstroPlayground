using UnityEditor;

namespace AsteroidProject
{
# if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(GameEventsType))]
    public sealed class GameEventsTypePropertyDrawer : EnumPropertyDrawer<GameEventsType>
    {
    }
#endif
}