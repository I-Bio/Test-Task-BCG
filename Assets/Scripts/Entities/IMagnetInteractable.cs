using UnityEngine;

namespace Entities
{
    public interface IMagnetInteractable
    {
        public int Value { get; }

        public void Interact(Vector3 playerPosition);
    }
}