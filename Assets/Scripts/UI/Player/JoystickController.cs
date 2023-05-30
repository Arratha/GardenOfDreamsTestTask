using System;

using UnityEngine;
using UnityEngine.EventSystems;


namespace UI.Player
{
    [RequireComponent(typeof(RectTransform))]
    public class JoystickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public static event Action<Vector2> MovePlayer;

        [SerializeField] private RectTransform _handle;
        private RectTransform _selfRectTranform;

        private float _radius;

        private bool _isPressed;

        private void Awake()
        {
            Initialize();
        }

        private void FixedUpdate()
        {
            MoveHandle();
        }

        private void Initialize()
        {
            _selfRectTranform = GetComponent<RectTransform>();

            _radius = (_selfRectTranform.rect.size.x - _handle.rect.size.x) / 2;
        }

        private void MoveHandle()
        {
            if (!_isPressed)
                return;

            Vector2 mousePosition = Input.mousePosition;
            Vector2 selfPosition = _selfRectTranform.position;

            Vector2 targetVector = (mousePosition - selfPosition).normalized;

            MovePlayer?.Invoke(targetVector);

            Vector2 newHandlePosition;

            if (Vector2.Distance(selfPosition, mousePosition) > _radius)
                newHandlePosition = selfPosition + targetVector * _radius;
            else
                newHandlePosition = mousePosition;

            _handle.position = newHandlePosition;
        }

        private void ResetHandle()
        {
            _isPressed = false;

            _handle.anchoredPosition = Vector2.zero;

            MovePlayer?.Invoke(Vector2.zero);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ResetHandle();
        }
    }
}