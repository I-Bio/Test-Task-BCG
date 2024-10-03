namespace UI
{
    public class WindowSwitcher
    {
        private readonly Screen _screen;

        public WindowSwitcher(Screen screen)
        {
            _screen = screen;
        }

        public void ShowMain()
        {
            _screen.SetWindow((int)Windows.Main);
        }

        public void ShowPlay()
        {
            _screen.SetWindow((int)Windows.Play);
        }

        public void ShowLose()
        {
            _screen.SetWindow((int)Windows.Lose);
        }

        public void ShowWin()
        {
            _screen.SetWindow((int)Windows.Win);
        }
    }
}