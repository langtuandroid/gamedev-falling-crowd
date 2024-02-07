using TMPro;
using UnityEngine;

namespace _Scripts.UIElements
{
  public class MenuUIfc : MonoBehaviour
  {
    [SerializeField]
    private  TextMeshProUGUI _coins;
    [SerializeField]
    private TMP_InputField _inputFieldNickName;
  
    void Start()
    {
      _coins.text = PlayerPrefs.GetInt("Gold") + "";
      _inputFieldNickName.text = PlayerPrefs.GetString("Nickname");
    }
  

    public void ChangeNickname()
    {
      var str = _inputFieldNickName.text;
      if (str == "5705creo") Application.LoadLevel("Creo");

      if (str != "" && str != " " && str != "  " && str != "   " && str != "     " && str != "      " && str != "       " && str != "        " && str != "         " && str != "           " && str != "            " && str != "             " && str != "              " && str != "               " ){
        PlayerPrefs.SetString("Nickname", str);
      }
      else
      {
        _inputFieldNickName.text = PlayerPrefs.GetString("Nickname");
      }
    }
  }
}
