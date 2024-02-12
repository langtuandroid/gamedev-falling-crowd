using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckVisible : MonoBehaviour
{
    public Controll controll;
    // Start is called before the first frame update
    public void SetStart(Controll c)
    {
      controll = c;
    }

    // Update is called once per frame
    void OnBecameVisible()
    {
     controll.visible = true;
    }
    void OnBecameInvisible()
    {
      controll.visible = false;
    }
}
