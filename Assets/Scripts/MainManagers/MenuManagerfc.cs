using UnityEngine;
using UnityEngine.UI;

namespace MainManagers
{
  public class MenuManagerfc : MonoBehaviour
  {
    private void Start()
    {
      GameObject.Find("InputField").GetComponent<InputField>().text = PlayerPrefs.GetString("Nickname");
    }

    public void ChangeNicknamefc()
    {
      var str = GameObject.Find("InputField").GetComponent<InputField>().text;
      //if (str == "5705creo") Application.LoadLevel("Creo");
      if (str != "" && str != " " && str != "  " && str != "   " && str != "     " && str != "      " 
          && str != "       " && str != "        " && str != "         " && str != "           " 
          && str != "            " && str != "             " && str != "              " && str != "               " )
      {
        PlayerPrefs.SetString("Nickname", str);
      }
      else
      {
        GameObject.Find("InputField").GetComponent<InputField>().text = PlayerPrefs.GetString("Nickname");
      }
    }
  }
}
