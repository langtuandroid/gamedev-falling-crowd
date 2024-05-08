using Integration;
using MainManagers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopDiamonds : MonoBehaviour
{
    [SerializeField]
    private CharacterSelector _characterSelector;
    [SerializeField] 
    private TextMeshProUGUI _diamondInfo;
    
    [SerializeField]
    private Button _buyPack1;
    [SerializeField]
    private Button _buyPack2;
    [SerializeField]
    private Button _buyPack3;
    [SerializeField]
    private Button _buyPack4;

    private IAPService _iapService;
    
    [Inject]
    private void Construct(IAPService iapService)
    {
        _iapService = iapService;
    }

    
    private void Awake()
    {
        _buyPack1.onClick.AddListener(BuyPack1);
        _buyPack2.onClick.AddListener(BuyPack2);
        _buyPack3.onClick.AddListener(BuyPack3);
        _buyPack4.onClick.AddListener(BuyPack4);
        _iapService.OnBuyDiamonds += UpdateDiamondInfo;
    }

    private void OnEnable()
    {
        UpdateDiamondInfo();
    }

    private void OnDestroy()
    {
        _buyPack1.onClick.RemoveListener(BuyPack1);
        _buyPack2.onClick.RemoveListener(BuyPack2);
        _buyPack3.onClick.RemoveListener(BuyPack3);
        _buyPack4.onClick.RemoveListener(BuyPack4);
        _iapService.OnBuyDiamonds -= UpdateDiamondInfo;
    }

    private void BuyPack1()
    {
        _iapService.BuyPack1();
    }
    private void BuyPack2()
    {
        _iapService.BuyPack2();
    }
    private void BuyPack3()
    {
        _iapService.BuyPack3();
    }
    private void BuyPack4()
    {
        _iapService.BuyPack4();
    }

    private void UpdateDiamondInfo()
    {
        _characterSelector.RefreshDiamondInfo();
        _diamondInfo.text = SaveLoadManager.LoadDiamonds().ToString();
    }
    
}
