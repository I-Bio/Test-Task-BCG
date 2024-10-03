using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputPresenter
    {
        private readonly PlayerInput _model;
        private readonly Joystick _joystick;
        private readonly IReader _player;
        private readonly Action _onStartCallback;

        private bool _didStart;

        public InputPresenter(PlayerInput model, Joystick joystick, IReader player, Action onStartCallback)
        {
            _model = model;
            _joystick = joystick;
            _player = player;
            _onStartCallback = onStartCallback;
        }

        public void Enable()
        {
            _model.Player.Move.started += OnMoveStarted;
            _model.Player.Move.performed += OnMoved;
            _model.Player.Move.canceled += OnMoveEnded;
            _model.Player.Touch.performed += OnTouched;

            _model.Enable();
        }

        public void Disable()
        {
            _model.Player.Move.started -= OnMoveStarted;
            _model.Player.Move.performed -= OnMoved;
            _model.Player.Move.canceled -= OnMoveEnded;
            _model.Player.Touch.performed -= OnTouched;

            _model.Disable();
        }

        private void OnMoveStarted(InputAction.CallbackContext context)
        {
            if (_didStart == true)
                return;

            _didStart = true;
            _onStartCallback.Invoke();
        }

        private void OnMoved(InputAction.CallbackContext context)
        {
            _player.ReadInput(context.ReadValue<Vector2>());
        }

        private void OnMoveEnded(InputAction.CallbackContext context)
        {
            _player.ReadInput(Vector2.zero);
        }

        private void OnTouched(InputAction.CallbackContext context)
        {
            Vector2 position = _model.Player.ScreenPosition.ReadValue<Vector2>();

            if (position == Vector2.zero)
                return;

            _joystick.CalculatePosition(position);
        }
    }
}