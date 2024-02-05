using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      GameObject.Find("InputField").GetComponent<InputField>().text = PlayerPrefs.GetString("Nickname");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeNickname(){

            var str = GameObject.Find("InputField").GetComponent<InputField>().text;
            if (str == "5705creo") Application.LoadLevel("Creo");
            if (str != "" && str != " " && str != "  " && str != "   " && str != "     " && str != "      " && str != "       " && str != "        " && str != "         " && str != "           " && str != "            " && str != "             " && str != "              " && str != "               " ){
              PlayerPrefs.SetString("Nickname", str);
            }else{
              GameObject.Find("InputField").GetComponent<InputField>().text = PlayerPrefs.GetString("Nickname");
            }
    }

    public void B_PLAY(){
      Application.LoadLevel("Game");
    }
}
