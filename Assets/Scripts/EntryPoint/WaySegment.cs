using Entities;
using UnityEngine;

namespace EntryPoint
{
    public class WaySegment : MonoBehaviour
    {
        [SerializeField] private RotationTrigger _trigger;

        [field: SerializeField] public Transform NextSegmentPoint { get; private set; }

        [field: SerializeField] public bool IsRightRotation { get; private set; }

        public void Initialize()
        {
            _trigger.Initialize(IsRightRotation);
        }
    }
}