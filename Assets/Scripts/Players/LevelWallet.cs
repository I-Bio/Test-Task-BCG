using System;
using System.Collections.Generic;
using System.Linq;

namespace Players
{
    public class LevelWallet
    {
        private const string ExceptionMessage = "Value must be greater than 0";
        private const int MinValue = 0;

        private readonly Dictionary<Stage, int> _stageEntries;

        private Stage _currentStage;
        private int _moneyCount;

        public LevelWallet(Dictionary<Stage, int> stageEntries)
        {
            _stageEntries = stageEntries;
            _currentStage = Stage.Poor;
            _moneyCount = _stageEntries[Stage.Poor];
        }

        public event Action<Stage> StageChanged;

        public event Action<int> MoneyChanged;

        public event Action Losing;

        public void AddMoney(int value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException($"{ExceptionMessage}: {nameof(value)}");

            _moneyCount += value;
            CompareEntries();
        }

        public void RemoveMoney(int value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException($"{ExceptionMessage}: {nameof(value)}");

            _moneyCount -= value;

            if (_moneyCount < 0)
                _moneyCount = 0;

            CompareEntries();
        }

        private void CompareEntries()
        {
            MoneyChanged?.Invoke(_moneyCount);

            Stage stage = _stageEntries.Last(pair => _moneyCount >= pair.Value).Key;

            if (_currentStage == stage)
                return;

            if (stage != Stage.Millionaire)
            {
                if (_moneyCount > MinValue && stage == Stage.Hobo)
                    return;

                _currentStage = stage;
                StageChanged?.Invoke(_currentStage);
            }

            if (_currentStage != Stage.Hobo)
                return;

            Losing?.Invoke();
        }
    }
}