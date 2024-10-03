using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Players;
using UnityEngine;

namespace Entities
{
    public abstract class Entity : MonoBehaviour, IInteractable
    {
        [SerializeField] private float _magnetDuration = 0.3f;
        [SerializeField] private bool _canMagnet;

        private Transform _transform;
        private CancellationToken _token;

        private void Awake()
        {
            _transform = transform;
            _token = destroyCancellationToken;
        }

        public async void Interact(Vector3 playerPosition)
        {
            if (_canMagnet == true)
                await _transform.DOMove(playerPosition, _magnetDuration).ToUniTask(cancellationToken: _token);

            gameObject.SetActive(false);
        }

        public abstract void Accept(IPlayerVisitor visitor);
    }
}