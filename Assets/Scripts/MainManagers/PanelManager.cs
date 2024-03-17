using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainManagers
{
   public class PanelManager : MonoBehaviour
   {
      [SerializeField] 
      private List<GameObject> _allPanels;
   
      [SerializeField] 
      private List<Button> _backToMenu;
      [SerializeField] 
      private Button _settings;
      [SerializeField] 
      private Button _shop;
      
      [SerializeField] 
      private Button _startGame;


      private void Awake()
      {
         foreach (var button in _backToMenu)
         {
            button.onClick.AddListener(BackToMainMenu);
         }
         _settings.onClick.AddListener(OpenSettingsMenu);
         _shop.onClick.AddListener(OpenShopMenu);
         _startGame.onClick.AddListener(StartGame);
      }

      
      private void OnDestroy()
      {
         foreach (var button in _backToMenu)
         {
            button.onClick.RemoveListener(BackToMainMenu);
         }
         _settings.onClick.RemoveListener(OpenSettingsMenu);
         _shop.onClick.AddListener(OpenShopMenu);
         _startGame.onClick.AddListener(StartGame);
      }
      
      private void BackToMainMenu()
      {
         DeactiveAllPanels();
         _allPanels[0].SetActive(true);
      }

      private void OpenSettingsMenu()
      {
         DeactiveAllPanels();
         _allPanels[1].SetActive(true);
      }
      
      private void OpenShopMenu()
      {
         DeactiveAllPanels();
         _allPanels[2].SetActive(true);
      }
      
      private void StartGame()
      {
         SceneManager.LoadScene("Game");
      }
      
      private void DeactiveAllPanels()
      {
         foreach (var panel in _allPanels)
         {
            panel.SetActive(false);
         }
      }
   }
}
