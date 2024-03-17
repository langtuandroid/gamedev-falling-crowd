using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MainManagers
{
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
                termsButton.onClick.AddListener(() => OpenUrl_Resplendent(UrlForTermsOfService));

            if (privacyButton != null)
                privacyButton.onClick.AddListener(() => OpenUrl_Resplendent(UrlForPrivacyPolicy));
        }

        private void OnDestroy()
        {
            if (termsButton != null)
                termsButton.onClick.RemoveListener(() => OpenUrl_Resplendent(UrlForTermsOfService));

            if (privacyButton != null)
                privacyButton.onClick.RemoveListener(() => OpenUrl_Resplendent(UrlForPrivacyPolicy));
        }

        private async void OpenUrl_Resplendent(string url)
        {
            if (_externalOpeningUrlDelayFlag_Resplendent) return;
            _externalOpeningUrlDelayFlag_Resplendent = true;
            await ApplicationUtils.OpenURLAsync(url);
            StartCoroutine(Magnanimous_WaitForSeconds(1, () => _externalOpeningUrlDelayFlag_Resplendent = false));
        }

        private IEnumerator Magnanimous_WaitForSeconds(float seconds, Action callback)
        {
            yield return new WaitForSeconds(seconds);
            callback?.Invoke();
        } 
    }
}


