using DG.Tweening;
using Players;
using UnityEngine;

namespace Entities
{
    public abstract class RotatableEntity : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform _left;
        [SerializeField] private Transform _right;
        [SerializeField] private float _animationDuration = 0.5f;

        public abstract void Accept(IPlayerVisitor visitor);

        public void Rotate(Vector3 rotation)
        {
            _left.DORotate(-rotation, _animationDuration);
            _right.DORotate(rotation, _animationDuration);
        }
    }
}