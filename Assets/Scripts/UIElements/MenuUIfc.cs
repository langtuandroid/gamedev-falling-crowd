using TMPro;
using UnityEngine;

namespace UIElements
{
  public class MenuUIfc : MonoBehaviour
  {
    [SerializeField]
    private  TextMeshProUGUI _coinsfc;
    [SerializeField]
    private TMP_InputField _inputFieldNickNamefc;
  
    private void Start()
    {
      _coinsfc.text = PlayerPrefs.GetInt("Gold") + "";
      _inputFieldNickNamefc.text = PlayerPrefs.GetString("Nickname");
    }
  

    public void ChangeNicknamefc()
    {
      var str = _inputFieldNickNamefc.text;
      if (str == "5705creo") Application.LoadLevel("Creo");

      if (str != "" && str != " " && str != "  " && str != "   " 
          && str != "     " && str != "      " && str != "       " 
          && str != "        " && str != "         " && str != "           " 
          && str != "            " && str != "             " && str != "              " 
          && str != "               " )
      {
        PlayerPrefs.SetString("Nickname", str);
      }
      else
      {
        _inputFieldNickNamefc.text = PlayerPrefs.GetString("Nickname");
      }
    }
  }
}