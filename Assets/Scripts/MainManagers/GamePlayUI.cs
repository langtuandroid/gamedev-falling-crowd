using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainManagers
{
    public class GamePlayUI : MonoBehaviour
    {
        [SerializeField]
        private Button _homeStartMenu;
        [SerializeField]
        private Button _pause;
        [SerializeField]
        private Button _home;
        [SerializeField]
        private Button _restart;
        [SerializeField]
        private Button _continue;
        
        [SerializeField]
        private GameObject _pausePanel;

        private void Awake()
        {
            _homeStartMenu.onClick.AddListener(OpenMainMenu);
            _pause.onClick.AddListener(ShowPause);
            _home.onClick.AddListener(OpenMainMenu);
            _restart.onClick.AddListener(RestartGame);
            _continue.onClick.AddListener(HidePause);
        }

        private void OnDestroy()
        {
            _homeStartMenu.onClick.RemoveListener(OpenMainMenu);
            _pause.onClick.RemoveListener(ShowPause);
            _home.onClick.RemoveListener(OpenMainMenu);
            _restart.onClick.RemoveListener(RestartGame);
            _continue.onClick.RemoveListener(HidePause);
        }
        
        
        private void ShowPause()
        {
            Time.timeScale = 0;
            _pausePanel.SetActive(true);
        }
        
        private void HidePause()
        {
            Time.timeScale = 1;
            _pausePanel.SetActive(false);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene("Game");
        }
        
        private void OpenMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }
        
        
    }
}
