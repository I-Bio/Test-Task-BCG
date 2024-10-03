using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class RestartButton : MonoBehaviour
    {
        private const int FirstScene = 0;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Reload);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Reload);
        }

        private void Reload()
        {
            SceneManager.LoadScene(FirstScene);
        }
    }
}