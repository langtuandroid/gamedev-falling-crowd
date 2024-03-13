using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OpenUrl : MonoBehaviour
{
    [SerializeField] private string UrlForTermsOfService;
    [SerializeField] private string UrlForPrivacyPolicy;
    
    [SerializeField] private Button termsButton;
    [SerializeField] private Button privacyButton;
    
    private bool _externalOpeningUrlDelayFlag_Resplendent = false;

    private void Awake()
    {
        if (termsButton != null)
        {
            termsButton.onClick.AddListener(OnTermsClicked_Resplendent());
        }

        if (privacyButton != null)
        {
            privacyButton.onClick.AddListener(OnPrivacyClicked_Resplendent());
        }
    }

    private void OnDestroy()
    {
        if (termsButton != null)
        {
            termsButton.onClick.RemoveListener(OnTermsClicked_Resplendent());
        }
        
        if (privacyButton != null)
        {
            privacyButton.onClick.RemoveListener(OnPrivacyClicked_Resplendent());
        }
    }

    private UnityAction OnTermsClicked_Resplendent()
    {
        void Terms_Resplendent()
        {
            OpenUrl_Resplendent(UrlForTermsOfService);
        }

        return Terms_Resplendent;
    }

    private UnityAction OnPrivacyClicked_Resplendent()
    {
        void Privacy_Resplendent()
        {
            OpenUrl_Resplendent(UrlForPrivacyPolicy);
        }

        return Privacy_Resplendent;
    }
    

    private void OpenUrl_Resplendent(string url)
    {
        if (_externalOpeningUrlDelayFlag_Resplendent) return;
        _externalOpeningUrlDelayFlag_Resplendent = true;
       // ApplicationUtilsSwift.OpenURLInAsyncMode(url).Forget();
       // CoroutineDispatcher.Magnanimous_Wait(1, () => { _externalOpeningUrlDelayFlag_Resplendent = false; });
    }
}