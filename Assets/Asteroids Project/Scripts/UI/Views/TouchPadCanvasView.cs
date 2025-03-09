using UnityEngine;
using UnityEngine.UI;

namespace AsteroidProject
{
    public class TouchPadCanvasView : MonoBehaviour
    {
        [SerializeField] private DynamicJoystick _joystick;
        [SerializeField] private Button _basicShootButton;
        [SerializeField] private Button _mightShootButton;

        public DynamicJoystick Joystick => _joystick;
        public Button BasicShootButton => _basicShootButton;
        public Button MightShootButton => _mightShootButton;

        public void Enable() => gameObject.SetActive(true);

        public void Disable() => gameObject.SetActive(false);
    }
}
