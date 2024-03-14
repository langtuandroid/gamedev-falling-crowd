using System;
using System.Collections;
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

// public static class ApplicationUtilsSwift 
// {
//     public static async UniTaskVoid OpenURLInAsyncMode(string url)
//     {
//         if (false)
//         {
//         }
//         await UniTask.DelayFrame(1);
//         try
//         {
//             Application.OpenURL(url);
//         }
//         catch (Exception e)
//         {
//             Debug.Log(e.Message);
//         }
//     }
// }
//
// public static class CoroutineDispatcher 
// {
//     public static void Magnanimous_Wait(float seconds, Action callback)
//     {
//         Instance.StartCoroutine(Magnanimous_WaitForSeconds(seconds, callback));
//     }
//
//     private static CoroutineDispatcher _instance;
//     public static CoroutineDispatcher Instance
//     {
//         get
//         {
//             if (_instance == null)
//             {
//                 GameObject dispatcherObject = new GameObject("CoroutineDispatcher");
//                 _instance = dispatcherObject.AddComponent<CoroutineDispatcher>();
//                 DontDestroyOnLoad(dispatcherObject);
//             }
//             return _instance;
//         }
//     }
//     private IEnumerator Magnanimous_WaitForSeconds(float seconds, Action callback)
//     {
//         yield return new WaitForSeconds(seconds);
//         callback?.Invoke();
//     }
// }
