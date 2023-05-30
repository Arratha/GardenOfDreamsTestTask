using System;

using UnityEngine;
using UnityEngine.EventSystems;


namespace UI.Player
{
    public class FireButtonController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        public static event Action Attack;

        private bool _isPressed;

        private void FixedUpdate()
        {
            if (_isPressed)
                Attack?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPressed = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressed = true;
        }
    }
}