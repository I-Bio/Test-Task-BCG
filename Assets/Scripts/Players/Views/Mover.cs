using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Input;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(Rigidbody))]
    public class Mover : MonoBehaviour, IReader
    {
        private const float MinValue = 0f;

        [SerializeField] private float _maxRotationAngle = 15f;
        [SerializeField] private float _forwardSpeed = 2f;
        [SerializeField] private float _horizontalSpeed = 1.5f;
        [SerializeField] private float _rotationStep = 0.01f;

        private Rigidbody _rigidbody;
        private Transform _transform;
        private Transform _rotationPoint;
        private Vector2 _input;
        private Vector3 _forward;
        private Vector3 _right;
        private Vector3 _targetDirection;

        private CancellationTokenSource _moveCancellation;
        private CancellationTokenSource _rotationCancellation;
        private Action _onStartMoveCallback;

        private void OnDestroy()
        {
            Dispose(_moveCancellation);
            Dispose(_rotationCancellation);
        }

        public void Initialize(Transform transform, Transform rotationPoint, Action onStartMoveCallback)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;
            _rotationPoint = rotationPoint;
            _forward = _transform.forward;
            _right = _transform.right;
            _onStartMoveCallback = onStartMoveCallback;
        }

        public void ReadInput(Vector2 input)
        {
            _input = input;
        }

        public void StartMove()
        {
            _onStartMoveCallback.Invoke();
            _moveCancellation = new CancellationTokenSource();
            _rotationCancellation = new CancellationTokenSource();
            Move().Forget();
            Rotate().Forget();
        }

        public void StopMove()
        {
            Dispose(_moveCancellation);
            Dispose(_rotationCancellation);
            _rigidbody.velocity = Vector3.zero;
        }

        public void RotateWay(bool isRightRotation)
        {
            if (isRightRotation == true)
            {
                _targetDirection = _right;
                (_forward, _right) = (_right, -_forward);
                return;
            }

            _targetDirection = -_right;
            (_forward, _right) = (-_right, _forward);
        }

        private void Dispose(CancellationTokenSource cancellation)
        {
            if (cancellation == null)
                return;

            if (cancellation.IsCancellationRequested == false)
                cancellation.Cancel();

            cancellation.Dispose();
        }

        private async UniTaskVoid Rotate()
        {
            while (_rotationCancellation.IsCancellationRequested == false)
            {
                if (_targetDirection != Vector3.zero)
                {
                    Quaternion look = Quaternion.LookRotation(new Vector3(_targetDirection.x, 0f, _targetDirection.z));
                    _transform.rotation = Quaternion.RotateTowards(_transform.rotation, look, _rotationStep);
                }

                await UniTask.NextFrame(PlayerLoopTiming.FixedUpdate, cancellationToken: _rotationCancellation.Token);
            }
        }

        private async UniTaskVoid Move()
        {
            while (_moveCancellation.IsCancellationRequested == false)
            {
                Vector3 direction = Vector3.zero;

                if (_input != Vector2.zero)
                {
                    direction = _right * _input.x * _horizontalSpeed;

                    _rigidbody.velocity = direction + _forward * _forwardSpeed;
                    _rotationPoint.eulerAngles = new Vector3(
                        MinValue,
                        Mathf.Clamp(
                            Vector3.SignedAngle(_forward, direction, Vector3.up),
                            -_maxRotationAngle,
                            _maxRotationAngle),
                        MinValue);
                }

                _rigidbody.velocity = direction + _forward * _forwardSpeed;

                await UniTask.NextFrame(PlayerLoopTiming.FixedUpdate, cancellationToken: _moveCancellation.Token);
            }
        }
    }
}