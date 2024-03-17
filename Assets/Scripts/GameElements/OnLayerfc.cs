using System;
using MainManagers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameElements
{
  public class OnLayerfc : MonoBehaviour
  {
    private Controllfc _playerControllfc;
    public TextMeshProUGUI nickfc;
    //private bool startedfc;
    

    public void SetPlayercOntroller(Controllfc playerControllfc)
    {
      _playerControllfc = playerControllfc;
      nickfc.text = _playerControllfc.nicknamefc;
      Color col = _playerControllfc.playerColorfc;
      col.a = 0.35f;
      GetComponent<Image>().color = col;
    }
    private void Update()
    {
      if (_playerControllfc == null)
      {
        Destroy(gameObject);
      }
    }
  }
}
