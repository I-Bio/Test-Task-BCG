using UnityEngine;

namespace UI
{
    public class Screen : MonoBehaviour
    {
        [SerializeField] private Window[] _windows;

        private Window _current;

        public void Initialize()
        {
            foreach (Window window in _windows)
                window.Initialize();
        }

        public void SetWindow(int id)
        {
            if (_current != null)
                _current.Close();

            _current = _windows[id];
            _current.Open();
        }
    }
}