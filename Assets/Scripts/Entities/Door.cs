using Players;
using UnityEngine;

namespace Entities
{
    public class Door : RotatableEntity, IDoor
    {
        [SerializeField] private Stage _target;
        [SerializeField] private Vector3 _rotation = new (0f, 90f, 0f);

        public bool TryInteract(Stage stage)
        {
            if (stage < _target)
                return false;

            Rotate(_rotation);
            return true;
        }

        public override void Accept(IPlayerVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}