using TMPro;
using UI;

namespace Players
{
    public class PlayerPresenter
    {
        private readonly LevelWallet _wallet;
        private readonly PlayerCollision _collision;
        private readonly Mover _mover;
        private readonly WindowSwitcher _switcher;
        private readonly EffectReproducer _effectReproducer;
        private readonly AudioReproducer _audioReproducer;
        private readonly ProgressBar _progressBar;
        private readonly PlayerAppearance _appearance;
        private readonly PopUpText _moneyIncrease;
        private readonly PopUpText _moneyDecrease;
        private readonly TMP_Text _score;

        public PlayerPresenter(
            LevelWallet wallet,
            PlayerCollision collision,
            Mover mover,
            WindowSwitcher switcher,
            EffectReproducer effectReproducer,
            AudioReproducer audioReproducer,
            PlayerAppearance appearance,
            ProgressBar progressBar,
            PopUpText moneyIncrease,
            PopUpText moneyDecrease,
            TMP_Text score)
        {
            _wallet = wallet;
            _collision = collision;
            _mover = mover;
            _switcher = switcher;
            _effectReproducer = effectReproducer;
            _audioReproducer = audioReproducer;
            _appearance = appearance;
            _progressBar = progressBar;
            _moneyIncrease = moneyIncrease;
            _moneyDecrease = moneyDecrease;
            _score = score;
        }

        public void Enable()
        {
            _collision.MoneyIncreasing += OnMoneyIncreasing;
            _collision.MoneyDecreasing += OnMoneyDecreasing;
            _collision.FlagInteracting += OnFlagInteracting;
            _collision.DoorInteracting += OnDoorInteracting;
            _collision.WayRotating += OnWayRotating;
            _collision.Winning += OnWinning;

            _wallet.StageChanged += OnStageChanged;
            _wallet.MoneyChanged += OnMoneyChanged;
            _wallet.Losing += OnLosing;
        }

        public void Disable()
        {
            _collision.MoneyIncreasing -= OnMoneyIncreasing;
            _collision.MoneyDecreasing -= OnMoneyDecreasing;
            _collision.FlagInteracting -= OnFlagInteracting;
            _collision.DoorInteracting -= OnDoorInteracting;
            _collision.WayRotating -= OnWayRotating;
            _collision.Winning -= OnWinning;

            _wallet.StageChanged -= OnStageChanged;
            _wallet.MoneyChanged -= OnMoneyChanged;
            _wallet.Losing -= OnLosing;
        }

        private void OnMoneyIncreasing(int value)
        {
            _wallet.AddMoney(value);
            _moneyIncrease.Play(value);
            _audioReproducer.Play(EffectType.MoneyIncreasing);
            _effectReproducer.Play(EffectType.MoneyIncreasing);
        }

        private void OnMoneyDecreasing(int value)
        {
            _wallet.RemoveMoney(value);
            _moneyDecrease.Play(value);
            _audioReproducer.Play(EffectType.MoneyDecreasing);
            _effectReproducer.Play(EffectType.MoneyDecreasing);
        }

        private void OnFlagInteracting()
        {
            _audioReproducer.Play(EffectType.Flag);
        }

        private void OnDoorInteracting()
        {
            _audioReproducer.Play(EffectType.Door);
        }

        private void OnWayRotating(bool isRightRotation)
        {
            _mover.RotateWay(isRightRotation);
        }

        private void OnWinning()
        {
            _mover.StopMove();
            _appearance.Play(PlayerAnimations.Idle);
            _audioReproducer.Play(EffectType.Win);
            _switcher.ShowWin();
        }

        private void OnStageChanged(Stage stage)
        {
            _progressBar.ChangeStage(stage);
            _appearance.Change(stage);
            _appearance.Play(PlayerAnimations.Spin);
            _collision.SetStage(stage);
            _audioReproducer.Play(EffectType.StageChanging);
        }

        private void OnMoneyChanged(int value)
        {
            _score.SetText(value.ToString());
            _progressBar.Change(value);
        }

        private void OnLosing()
        {
            _mover.StopMove();
            _appearance.Play(PlayerAnimations.Disappoint);
            _switcher.ShowLose();
        }
    }
}