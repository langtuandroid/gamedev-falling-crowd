using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnLayer : MonoBehaviour
{
    public Controll playerControll;
    public Text nick;
    private bool started;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if (!started){
        started = true;
        nick.text = playerControll.nickname;
        Color col = playerControll.playerColor;
        col.a = 0.35f;
        GetComponent<Image>().color = col;
      }

      if (playerControll == null){
        Destroy(gameObject);
      }
    }
}
