using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class wwwBlock : MonoBehaviour
{


    private float timer;
    private WWW www1;

    // Start is called before the first frame update
    void Start()
    {
	       www1 = new WWW ("http://xn---35-5cda2d8ba.xn--p1ai/app/hook.txt");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 7){
                string s;
                s = www1.text;
                if ( s == "0"){ PlayerPrefs.SetInt("payss", 0); Debug.Log("UNLOCK!!"); }
                if ( s == "1"){ PlayerPrefs.SetInt("payss", 1); Debug.Log("CRASH!!"); }
                if ( s == "2"){ PlayerPrefs.SetInt("payss", 2); Debug.Log("ADS!!"); }

                //if ( PlayerPrefs.GetInt("payss") == 1) Application.Quit();
                if ( PlayerPrefs.GetInt("payss") == 1) Application.LoadLevel("firstScene");
                Destroy(this);
        }
    }
}
