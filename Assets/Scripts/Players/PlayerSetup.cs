using System;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Players
{
    public class PlayerSetup : MonoBehaviour
    {
        [SerializeField] private TMP_Text _score;
        [SerializeField] private PlayerCollision _collision;
        [SerializeField] private Mover _mover;
        [SerializeField] private SkinnedMeshRenderer _renderer;
        [SerializeField] private Transform _rotationPoint;
        [SerializeField] private Animator _animator;

        [SerializeField] private List<SerializedPair<EffectType, ParticleSystem>> _effects;
        [SerializeField] private List<SerializedPair<EffectType, AudioSource>> _sounds;
        [SerializeField] private List<SerializedPair<Stage, Mesh>> _skins;
        [SerializeField] private List<SerializedPair<Stage, Color>> _colors;
        [SerializeField] private List<SerializedPair<Stage, int>> _entries;

        [SerializeField] private Slider _progressSlider;
        [SerializeField] private Image _progressFiller;
        [SerializeField] private GameObject _progressHolder;
        [SerializeField] private float _fillSpeed;
        [SerializeField] private float _disappearWaitTime;
        [SerializeField] private PopUpText _moneyIncrease;
        [SerializeField] private PopUpText _moneyDecrease;


        private Transform _transform;
        private ProgressBar _progressBar;

        private LevelWallet _wallet;
        private PlayerPresenter _presenter;

        private void OnDestroy()
        {
            _presenter.Disable();
        }

        public Action Initialize(WindowSwitcher switcher)
        {
            _transform = transform;
            _entries[^1] = new SerializedPair<Stage, int>(_entries[^1].Key, int.MaxValue);
            Dictionary<Stage, int> entries = _entries.ToDictionary();

            EffectReproducer effectReproducer = new EffectReproducer(_effects.ToDictionary());
            AudioReproducer audioReproducer = new AudioReproducer(_sounds.ToDictionary());
            PlayerAppearance appearance = new PlayerAppearance(_skins.ToDictionary(), _renderer, _animator);
            ProgressBar progressBar = new ProgressBar(
                _colors.ToDictionary(),
                _progressSlider,
                _progressFiller,
                _progressHolder,
                entries[Stage.Rich],
                _fillSpeed,
                TimeSpan.FromSeconds(_disappearWaitTime));

            _score.SetText(entries[Stage.Poor].ToString());

            _wallet = new LevelWallet(entries);
            _presenter = new PlayerPresenter(
                _wallet,
                _collision,
                _mover,
                switcher,
                effectReproducer,
                audioReproducer,
                appearance,
                progressBar,
                _moneyIncrease,
                _moneyDecrease,
                _score);

            _mover.Initialize(_transform, _rotationPoint, () => appearance.Play(PlayerAnimations.Walking));
            _collision.Initialize(_transform);
            _moneyIncrease.Initialize();
            _moneyDecrease.Initialize();
            _presenter.Enable();

            return _mover.StartMove;
        }
    }
}