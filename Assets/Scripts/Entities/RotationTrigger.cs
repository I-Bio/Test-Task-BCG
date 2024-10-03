using Players;
using UnityEngine;

namespace Entities
{
    public class RotationTrigger : MonoBehaviour, IInteractable, IRotationTrigger
    {
        public bool IsRightRotation { get; private set; }

        public void Initialize(bool isRightRotation)
        {
            IsRightRotation = isRightRotation;
        }

        public void Accept(IPlayerVisitor visitor)
        {
            visitor.Visit(this);
            gameObject.SetActive(false);
        }
    }
}