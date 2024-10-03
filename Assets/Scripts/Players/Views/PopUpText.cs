using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Players
{
    public class PopUpText : MonoBehaviour
    {
        [SerializeField] private Transform _targetPoint;
        [SerializeField] private Transform _transform;
        [SerializeField] private GameObject _body;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _moveDuration;

        private int _value;
        private bool _isPlaying;
        private CancellationTokenSource _cancellation;
        private float _yPosition;

        private void OnDestroy()
        {
            if (_cancellation == null)
                return;

            if (_cancellation.IsCancellationRequested == false)
                _cancellation.Cancel();

            _cancellation.Dispose();
        }

        public void Initialize()
        {
            _yPosition = _transform.position.y;
        }

        public void Play(int value)
        {
            if (_isPlaying == true)
            {
                AddValue(value);
                return;
            }

            _value = value;
            _isPlaying = true;
            Move().Forget();
        }

        private void AddValue(int value)
        {
            _value += value;
            ResetAnimation();
            Move().Forget();
        }

        private void ResetAnimation()
        {
            _cancellation.Dispose();
            Vector3 position = _transform.position;
            _transform.position = new Vector3(position.x, _yPosition, position.z);
        }

        private async UniTaskVoid Move()
        {
            _cancellation = new CancellationTokenSource();
            _text.SetText(_value.ToString());
            _body.SetActive(true);

            await _transform.DOMove(_targetPoint.position, _moveDuration)
                .ToUniTask(cancellationToken: _cancellation.Token);

            _body.SetActive(false);
            _value = 0;
            _isPlaying = false;
        }
    }
}