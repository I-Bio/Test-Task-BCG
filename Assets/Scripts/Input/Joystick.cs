using System;
using UnityEngine;

namespace Input
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Joystick : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private RectTransform _parent;
        [SerializeField] private RectTransform _holderTransform;

        public void CalculatePosition(Vector2 position)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(_parent, position, _camera) == false)
                return;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _parent,
                    position,
                    _camera,
                    out Vector2 localPosition) == false)
                return;

            _holderTransform.anchoredPosition = localPosition;
        }
    }
}