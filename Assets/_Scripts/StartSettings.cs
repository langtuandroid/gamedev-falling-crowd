using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("firstStart") != 1){
          PlayerPrefs.SetInt("firstStart", 1);
          PlayerPrefs.SetString("Nickname", "Player"+Random.Range(11212, 99999) );

        }

    }
}
