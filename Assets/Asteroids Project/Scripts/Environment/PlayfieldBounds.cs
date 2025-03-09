using UnityEngine;
using Zenject;

namespace AsteroidProject
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayfieldBounds : MonoBehaviour
    {
        private BoxCollider2D _collider;

        private float _offset;

        [Inject]
        private void Construct(GameCore gameCore)
        {
            _offset = gameCore.GameCoreData.PlayfieldOffset;
        }

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();

            SetColliderSize();
        }

        private void SetColliderSize()
        {
            Vector2 leftDownCorner = Camera.main.ScreenToWorldPoint(Vector2.zero);
            Vector2 rightUpCorner = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            _collider.size = new Vector2(rightUpCorner.x - leftDownCorner.x + _offset, rightUpCorner.y - leftDownCorner.y + _offset);
        }
    }
}
