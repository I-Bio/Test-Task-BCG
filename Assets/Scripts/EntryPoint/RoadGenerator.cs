using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EntryPoint
{
    public class RoadGenerator : MonoBehaviour
    {
        private const int MinValue = 0;

        [SerializeField] private List<WaySegment> _segmentTemplates;
        [SerializeField] private FinishSegment _finishTemplate;
        [SerializeField] private WaySegment _startTemplate;
        [SerializeField] private Transform _wayHolder;
        [SerializeField] private int _segmentCount;
        [SerializeField] private Vector3 _rightRotation;
        [SerializeField] private Vector3 _leftRotation;

        public void Initialize()
        {
            WaySegment last = Instantiate(_startTemplate, Vector3.zero, Quaternion.identity, _wayHolder);
            last.Initialize();

            for (int i = 0; i < _segmentCount; i++)
            {
                last = Instantiate(
                    FindTemplate(last.IsRightRotation),
                    last.NextSegmentPoint);

                last.Initialize();
            }

            Instantiate(
                _finishTemplate,
                last.NextSegmentPoint);
        }

        private WaySegment FindTemplate(bool isRightRotation)
        {
            WaySegment[] available = _segmentTemplates.Where(item => item.IsRightRotation != isRightRotation).ToArray();
            return available[Random.Range(MinValue, available.Length)];
        }
    }
}