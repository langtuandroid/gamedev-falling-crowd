using Integration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MainManagers
{
    public class CharacterSelector : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _coinsText;
        [SerializeField] 
        private TextMeshProUGUI _diamondsText;
        [SerializeField] 
        private TextMeshProUGUI _adsCountText;
        [SerializeField] 
        private GameObject _priceCoinsPanel;
        [SerializeField] 
        private GameObject _priceDiamondsPanel;
        [SerializeField] 
        private TextMeshProUGUI _priceCoins;
        [SerializeField] 
        private TextMeshProUGUI _priceDiamonds;
        [SerializeField] 
        private Button _unlock;
        [SerializeField] 
        private Button _unlockAd;
        [SerializeField] 
        private Button _select;
        [SerializeField] 
        private TextMeshProUGUI _selectText;
        [SerializeField] 
        private Button _next;
        [SerializeField] 
        private Button _preview;
        [SerializeField] 
        private bool _isOpen;
        [SerializeField] 
        private TypeUnlock _typeUnlock;
        [SerializeField] 
        private CharacterHolder _characterHolder;
        
        private int _coins;
        private int _diamonds;
        private int _price;
        private int _presentCharacterIndex;
        private int _selectedCharacterIndex;

        private AdMobController _adMobController;
        private RewardedAdController _rewardedAdController;
        [Inject]
        private void Construct(AdMobController adMobController, RewardedAdController rewardedAdController)
        {
            _adMobController = adMobController;
            _rewardedAdController = rewardedAdController;
        }
        private void Awake()
        {
            _next.onClick.AddListener(NextCharacter);
            _preview.onClick.AddListener(PreviewCharacter);
            _unlock.onClick.AddListener(UnlockCharacter);
            _unlockAd.onClick.AddListener(WatchRevarded);
            _select.onClick.AddListener(SelectCharacter);
            _rewardedAdController.GetRewarded += CounterAdIncrement;
        }

        private void OnDestroy()
        {
            _next.onClick.RemoveListener(NextCharacter);
            _preview.onClick.RemoveListener(PreviewCharacter);
            _unlock.onClick.RemoveListener(UnlockCharacter);
            _unlockAd.onClick.RemoveListener(WatchRevarded);
            _select.onClick.RemoveListener(SelectCharacter);
            _rewardedAdController.GetRewarded -= CounterAdIncrement;
        }

        private void CounterAdIncrement()
        {
            _characterHolder.CharacterDatas[_presentCharacterIndex].AdsWatchCounter++;
            _adsCountText.text = $"{_characterHolder.CharacterDatas[_presentCharacterIndex].AdsWatchCounter} / 5";
        }

        private void OnEnable()
        {
            _presentCharacterIndex = SaveLoadManager.LoadCharacter();
            LoadCharacterData(_presentCharacterIndex);
            UpdatePanelView();
        }

        private void LoadCharacterData(int index)
        {
            _characterHolder.CharacterModels[index].SetActive(true);
            _price = _characterHolder.CharacterDatas[index].Price;
            _priceCoins.text = $"{_price}";
            _priceDiamonds.text = $"{_price}";
        }

        public void ADD50()
        {
            int coin =  SaveLoadManager.LoadCoins();
            SaveLoadManager.SaveCoins(coin+50);
            UpdatePanelView();
        }

        private void RefreshCoinInfo()
        {
            _coins = SaveLoadManager.LoadCoins();
            _coinsText.text = $"{_coins}";
        }
        public void RefreshDiamondInfo()
        {
            _diamonds = SaveLoadManager.LoadDiamonds();
            _diamondsText.text = $"{_diamonds}";
        }
        private void UpdatePanelView()
        {
            RefreshCoinInfo();
            RefreshDiamondInfo();
            
            _presentCharacterIndex = SaveLoadManager.LoadCharacter();
            _selectedCharacterIndex = SaveLoadManager.LoadChoseCharacter();
            _isOpen = _characterHolder.CharacterDatas[_presentCharacterIndex].IsUnlockStatus;
            _typeUnlock = _characterHolder.CharacterDatas[_presentCharacterIndex].TypeUnlock;
            if (_isOpen)
            {
                _priceCoinsPanel.SetActive(false);
                _priceDiamondsPanel.SetActive(false);
                _unlock.gameObject.SetActive(false);
                _unlockAd.gameObject.SetActive(false);
                
                _select.gameObject.SetActive(true);
                if (_presentCharacterIndex == _selectedCharacterIndex)
                {
                    _selectText.text = "SELECTED";
                }
                else
                {
                    _selectText.text = "SELECT";
                }
            }
            else
            {
                if (_typeUnlock == TypeUnlock.ForCoins)
                {
                    _priceCoinsPanel.SetActive(true);
                    _priceDiamondsPanel.SetActive(false);
                    _select.gameObject.SetActive(false);
                    _unlock.gameObject.SetActive(true);
                    _unlockAd.gameObject.SetActive(false);
                    if (_coins >= _price)
                    {
                        _unlock.interactable = true;
                    }
                    else
                    {
                        _unlock.interactable = false;
                    }
                }
                else if(_typeUnlock == TypeUnlock.ForDiamonds)
                {
                    _priceCoinsPanel.SetActive(false);
                    _priceDiamondsPanel.SetActive(true);
                    _select.gameObject.SetActive(false);
                    _unlock.gameObject.SetActive(true);
                    _unlockAd.gameObject.SetActive(false);
                    if (_diamonds >= _price)
                    {
                        _unlock.interactable = true;
                    }
                    else
                    {
                        _unlock.interactable = false;
                    }
                }
                else if(_typeUnlock == TypeUnlock.ForAds)
                {
                    _priceCoinsPanel.SetActive(false);
                    _priceDiamondsPanel.SetActive(false);
                    _select.gameObject.SetActive(false);
                    _unlock.gameObject.SetActive(false);
                    _unlockAd.gameObject.SetActive(true);
                    _adsCountText.text = $"{_characterHolder.CharacterDatas[_presentCharacterIndex].AdsWatchCounter} / 5";
                    if (_characterHolder.CharacterDatas[_presentCharacterIndex].AdsWatchCounter >= 5)
                    {
                        _unlockAd.gameObject.SetActive(false);
                        _unlock.gameObject.SetActive(true);
                        _unlock.interactable = true;
                    }
                    else
                    {
                        _unlockAd.gameObject.SetActive(true);
                        _unlock.gameObject.SetActive(false);
                        _unlock.interactable = false;
                    }
                }
            }
        }

        private void NextCharacter()
        {
           _presentCharacterIndex = SaveLoadManager.LoadCharacter();
           int indexCharacter = _presentCharacterIndex;
           if (_presentCharacterIndex < _characterHolder.CharacterModels.Count - 1)
           {
               indexCharacter++;
           }
           else
           {
               indexCharacter = 0;
           }
           SwitchCharacter(indexCharacter);
           UpdatePanelView();
        }
        
        private void PreviewCharacter()
        {
            _presentCharacterIndex = SaveLoadManager.LoadCharacter();
            int indexCharacter = _presentCharacterIndex;
            if (_presentCharacterIndex <= 0)
            {
                indexCharacter = _characterHolder.CharacterModels.Count - 1;
            }
            else
            {
                indexCharacter--;
            }
            SwitchCharacter(indexCharacter);
            UpdatePanelView();
        }

        private void SwitchCharacter(int index)
        {
            Debug.Log("Index = " + index);
            foreach (var model in _characterHolder.CharacterModels)
            {
                model.SetActive(false);
            }
            LoadCharacterData(index);
            SaveLoadManager.SaveCharacter(index);
            UpdatePanelView();
        }

        private void WatchRevarded()
        {
            if (_characterHolder.CharacterDatas[_presentCharacterIndex].AdsWatchCounter >= 5)
            {
                _characterHolder.CharacterDatas[_presentCharacterIndex].IsUnlockStatus = true;
            }
            else
            {
                _adMobController.ShowRewardedAd();
            }
            UpdatePanelView();
        }
        
        private void UnlockCharacter()
        {
            if (_typeUnlock == TypeUnlock.ForCoins)
            {
                if (_coins >= _price)
                {
                    _coins -= _price;
                    SaveLoadManager.SaveCoins(_coins);
                    _characterHolder.CharacterDatas[_presentCharacterIndex].IsUnlockStatus = true;
                    RefreshCoinInfo();
                }
            }
            if (_typeUnlock == TypeUnlock.ForDiamonds)
            {
                if (_diamonds >= _price)
                {
                    SaveLoadManager.SaveDiamonds(-_price);
                    _characterHolder.CharacterDatas[_presentCharacterIndex].IsUnlockStatus = true;
                    RefreshDiamondInfo();
                }
               
            }
            if (_typeUnlock == TypeUnlock.ForAds)
            {
                if (_characterHolder.CharacterDatas[_presentCharacterIndex].AdsWatchCounter >= 5)
                {
                    _characterHolder.CharacterDatas[_presentCharacterIndex].IsUnlockStatus = true;
                }
            }
            
            UpdatePanelView();
        }

        private void SelectCharacter()
        {
            _presentCharacterIndex = SaveLoadManager.LoadCharacter();
            SaveLoadManager.SaveChoseCharacter(_presentCharacterIndex);
            UpdatePanelView();
            _selectText.text = "SELECTED";
        }
        
    }
}
