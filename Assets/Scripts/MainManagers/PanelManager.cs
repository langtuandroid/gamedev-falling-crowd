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
      private Button _skinshop;
      [SerializeField] 
      private List<Button> _shopDiamonds;
      
      [SerializeField] 
      private Button _startGame;


      private void Awake()
      {
         foreach (var button in _backToMenu)
         {
            button.onClick.AddListener(BackToMainMenu);
         }
         _settings.onClick.AddListener(OpenSettingsMenu);
         _skinshop.onClick.AddListener(OpenSkinShopMenu);
         _startGame.onClick.AddListener(StartGame);
         foreach (var buttonshop in _shopDiamonds)
         {
            buttonshop.onClick.AddListener(OpenShopMenu);
         }
      }

      
      private void OnDestroy()
      {
         foreach (var button in _backToMenu)
         {
            button.onClick.RemoveListener(BackToMainMenu);
         }
         _settings.onClick.RemoveListener(OpenSettingsMenu);
         _skinshop.onClick.RemoveListener(OpenSkinShopMenu);
         _startGame.onClick.RemoveListener(StartGame);
         foreach (var buttonshop in _shopDiamonds)
         {
            buttonshop.onClick.RemoveListener(OpenShopMenu);
         }
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
      
      private void OpenSkinShopMenu()
      {
         DeactiveAllPanels();
         _allPanels[2].SetActive(true);
      }
      private void OpenShopMenu()
      {
         DeactiveAllPanels();
         _allPanels[3].SetActive(true);
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
