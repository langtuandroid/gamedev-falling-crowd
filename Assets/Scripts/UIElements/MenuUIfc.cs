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
      if (_coinsfc != null)
      {
        _coinsfc.text = PlayerPrefs.GetInt("Gold") + "";
      }

      if (_inputFieldNickNamefc != null)
      {
        _inputFieldNickNamefc.text = PlayerPrefs.GetString("Nickname");
      }
     
    }
  

    public void ChangeNicknamefc()
    {
      var str = _inputFieldNickNamefc.text;
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
