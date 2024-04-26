using System.Collections;
using System.Collections.Generic;
using Integration;
using UnityEngine;

public class CharacterButton : MonoBehaviour
{
    public int characterIndex;

    public void WatchAdAndUnlockCharacter()
    {
        // if (AdMobController.Instance.CanUnlockCharacter(characterIndex))
        // {
        //     AdManager.Instance.UnlockCharacter(characterIndex);
        // }
        // else
        // {
        //     AdManager.Instance.WatchAdForCharacter(characterIndex);
        // }
    }
}
