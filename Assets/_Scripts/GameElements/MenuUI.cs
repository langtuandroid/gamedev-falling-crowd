using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Text t_coins;
    // Start is called before the first frame update
    void Start()
    {
      t_coins.text = PlayerPrefs.GetInt("Gold") + "";

        GameObject.Find("InputField").GetComponent<InputField>().text = PlayerPrefs.GetString("Nickname");
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
}
