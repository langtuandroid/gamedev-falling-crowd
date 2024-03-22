using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainManagers
{
    public class CharacterSelector : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _coinsText;
        [SerializeField] 
        private GameObject _pricePanel;
        [SerializeField] 
        private TextMeshProUGUI _priceValue;
        [SerializeField] 
        private Button _unlock;
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
        private CharacterHolder _characterHolder;
        
        private int _coins;
        private int _price;
        private int _presentCharacterIndex;
        private int _selectedCharacterIndex;
        private void Awake()
        {
            _next.onClick.AddListener(NextCharacter);
            _preview.onClick.AddListener(PreviewCharacter);
            _unlock.onClick.AddListener(UnlockCharacter);
            _select.onClick.AddListener(SelectCharacter);
        }

        private void OnDestroy()
        {
            _next.onClick.RemoveListener(NextCharacter);
            _preview.onClick.RemoveListener(PreviewCharacter);
            _unlock.onClick.RemoveListener(UnlockCharacter);
            _select.onClick.RemoveListener(SelectCharacter);
        }

        private void Start()
        {
            _presentCharacterIndex = SaveLoadManager.LoadCharacter();
            LoadCharacterData(_presentCharacterIndex);
            UpdatePanelView();
        }

        private void LoadCharacterData(int index)
        {
            _characterHolder.CharacterModels[index].SetActive(true);
            _price = _characterHolder.CharacterDatas[index].Price;
            _priceValue.text = $"{_price}";
        }

        public void ADD50()
        {
            int coin =  SaveLoadManager.LoadCoins();
            SaveLoadManager.SaveCoins(coin+50);
            UpdatePanelView();
        }

        private void UpdatePanelView()
        {
            _coins = SaveLoadManager.LoadCoins();
            _coinsText.text = $"{_coins}";
            _presentCharacterIndex = SaveLoadManager.LoadCharacter();
            _selectedCharacterIndex = SaveLoadManager.LoadChoseCharacter();
            _isOpen = _characterHolder.CharacterDatas[_presentCharacterIndex].IsUnlockStatus;
            if (_isOpen)
            {
                _pricePanel.SetActive(false);
                _select.gameObject.SetActive(true);
                _unlock.gameObject.SetActive(false);
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
                _pricePanel.SetActive(true);
                _select.gameObject.SetActive(false);
                _unlock.gameObject.SetActive(true);
                if (_coins >= _price)
                {
                    _unlock.interactable = true;
                }
                else
                {
                    _unlock.interactable = false;
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

        private void UnlockCharacter()
        {
            if (_coins >= _price)
            {
                _coins -= _price;
                SaveLoadManager.SaveCoins(_coins);
                _characterHolder.CharacterDatas[_presentCharacterIndex].IsUnlockStatus = true;
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
