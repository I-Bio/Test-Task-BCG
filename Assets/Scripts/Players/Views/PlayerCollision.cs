using System;
using Entities;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(Collider))]
    public class PlayerCollision : MonoBehaviour, IPlayerVisitor
    {
        private Transform _transform;
        private Stage _current;

        public event Action<int> MoneyIncreasing;

        public event Action<int> MoneyDecreasing;

        public event Action FlagInteracting;

        public event Action DoorInteracting;

        public event Action Winning;

        public event Action<bool> WayRotating;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IInteractable interactable) == false)
                return;

            interactable.Accept(this);
        }

        public void Initialize(Transform transform)
        {
            _transform = transform;
            _current = Stage.Poor;
        }

        public void SetStage(Stage stage)
        {
            _current = stage;
        }

        public void Visit(IObstacle obstacle)
        {
            MoneyDecreasing?.Invoke(obstacle.Value);
            obstacle.Interact(_transform.position);
        }

        public void Visit(IMoney money)
        {
            MoneyIncreasing?.Invoke(money.Value);
            money.Interact(_transform.position);
        }

        public void Visit(IFlag flag)
        {
            FlagInteracting?.Invoke();
            flag.Interact();
        }

        public void Visit(IDoor door)
        {
            if (door.TryInteract(_current) == true)
            {
                DoorInteracting?.Invoke();
                return;
            }

            Winning?.Invoke();
        }

        public void Visit(IRotationTrigger trigger)
        {
            WayRotating?.Invoke(trigger.IsRightRotation);
        }
    }
}