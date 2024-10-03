using Players;
using UnityEngine;

namespace Entities
{
    public class Flag : RotatableEntity, IFlag
    {
        public void Interact()
        {
            Rotate(Vector3.zero);
        }

        public override void Accept(IPlayerVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}