using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class GuideControl : MonoBehaviour
    {
        [SerializeField] private RectTransform _firstPoint;
        [SerializeField] private RectTransform _secondPoint;
        [SerializeField] private RectTransform _hand;
        [SerializeField] private RectTransform _parent;
        [SerializeField] private float _duration;

        public void Initialize()
        {
            Vector3 position = _parent.TransformPoint(_secondPoint.localPosition);
            _hand.position = _firstPoint.position;
            _hand.DOMove(position, _duration).SetLoops(-1, LoopType.Yoyo);
        }
    }
}