using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Window : MonoBehaviour
    {
        private const float OffAlpha = 0f;
        private const float OnAlpha = 1f;

        private CanvasGroup _group;

        public void Initialize()
        {
            _group = GetComponent<CanvasGroup>();
        }

        public void Open()
        {
            if (_group == null)
                return;

            _group.alpha = OnAlpha;
            _group.interactable = true;
            _group.blocksRaycasts = true;
        }

        public void Close()
        {
            if (_group == null)
                return;

            _group.alpha = OffAlpha;
            _group.interactable = false;
            _group.blocksRaycasts = false;
        }
    }
}