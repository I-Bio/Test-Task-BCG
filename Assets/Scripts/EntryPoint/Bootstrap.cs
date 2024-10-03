using System;
using Input;
using Players;
using UI;
using UnityEngine;
using Screen = UI.Screen;

namespace EntryPoint
{
    [RequireComponent(typeof(RoadGenerator))]
    [RequireComponent(typeof(InputSetup))]
    [RequireComponent(typeof(Screen))]
    [RequireComponent(typeof(GuideControl))]
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private PlayerSetup _player;

        private RoadGenerator _roadGenerator;
        private InputSetup _input;
        private Screen _screen;
        private GuideControl _guide;

        private void Awake()
        {
            _roadGenerator = GetComponent<RoadGenerator>();
            _input = GetComponent<InputSetup>();
            _screen = GetComponent<Screen>();
            _guide = GetComponent<GuideControl>();

            WindowSwitcher switcher = new WindowSwitcher(_screen);

            _roadGenerator.Initialize();
            Action onStart = _player.Initialize(switcher);
            onStart += switcher.ShowPlay;

            _input.Initialize(onStart);
            _screen.Initialize();

            switcher.ShowMain();
            _guide.Initialize();
        }
    }
}