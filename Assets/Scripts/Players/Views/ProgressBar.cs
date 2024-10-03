using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Players
{
    public class ProgressBar : IDisposable
    {
        private readonly Dictionary<Stage, Color> _colors;
        private readonly Slider _slider;
        private readonly Image _filler;
        private readonly GameObject _holder;
        private readonly float _maxEntry;
        private readonly float _fillSpeed;
        private readonly TimeSpan _waitTime;

        private Stage _current;
        private CancellationTokenSource _cancellationWaiting;
        private CancellationTokenSource _cancellationFilling;

        public ProgressBar(
            Dictionary<Stage, Color> colors,
            Slider slider,
            Image filler,
            GameObject holder,
            float maxEntry,
            float fillSpeed,
            TimeSpan waitTime)
        {
            _colors = colors;
            _slider = slider;
            _filler = filler;
            _maxEntry = maxEntry;
            _fillSpeed = fillSpeed;
            _holder = holder;
            _waitTime = waitTime;
        }

        public void Dispose()
        {
            Dispose(_cancellationWaiting);
            Dispose(_cancellationFilling);
        }

        public void ChangeStage(Stage stage)
        {
            Dispose(_cancellationWaiting);
            _current = stage;
            _filler.color = _colors[_current];
            Disappear().Forget();
        }

        public void Change(int value)
        {
            Dispose();
            Fill(value).Forget();
        }

        private void Dispose(CancellationTokenSource cancellation)
        {
            if (cancellation == null)
                return;

            if (cancellation.IsCancellationRequested == false)
                cancellation.Cancel();

            cancellation.Dispose();
        }

        private async UniTaskVoid Disappear()
        {
            _cancellationWaiting = new CancellationTokenSource();

            await UniTask.Delay(_waitTime, cancellationToken: _cancellationWaiting.Token);

            _holder.SetActive(false);
        }

        private async UniTaskVoid Fill(int value)
        {
            _holder.SetActive(true);
            _cancellationFilling = new CancellationTokenSource();
            float target = value / _maxEntry;

            while (_slider.value < target)
            {
                _slider.value = Mathf.Lerp(_slider.value, target, _fillSpeed);
                await UniTask.NextFrame(cancellationToken: _cancellationFilling.Token);
            }

            Disappear().Forget();
        }
    }
}