using Players;
using UnityEngine;

namespace Entities
{
    public class Obstacle : Entity, IObstacle
    {
        [field: SerializeField] public int Value { get; private set; }

        public override void Accept(IPlayerVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}