using System;
using Players;
using UnityEngine;

namespace Input
{
    public class InputSetup : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Mover _player;

        private PlayerInput _model;
        private InputPresenter _presenter;

        private void OnDestroy()
        {
            _presenter.Disable();
        }

        public void Initialize(Action onStartCallback)
        {
            _model = new PlayerInput();
            _presenter = new InputPresenter(_model, _joystick, _player, onStartCallback);
            _presenter.Enable();
        }
    }
}