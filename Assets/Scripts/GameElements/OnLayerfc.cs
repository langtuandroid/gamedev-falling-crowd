using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameElements
{
  public class OnLayerfc : MonoBehaviour
  {
    public Controll playerControllfc;
    public TextMeshProUGUI nickfc;
    private bool startedfc;
  
    private void Update()
    {
      if (!startedfc)
      {
        startedfc = true;
        nickfc.text = playerControllfc.nickname;
        Color col = playerControllfc.playerColor;
        col.a = 0.35f;
        GetComponent<Image>().color = col;
      }

      if (playerControllfc == null){
        Destroy(gameObject);
      }
    }
  }
}
