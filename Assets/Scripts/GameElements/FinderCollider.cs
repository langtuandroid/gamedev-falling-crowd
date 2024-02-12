using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinderCollider : MonoBehaviour
{
    private Controll controll;
    public GameObject objectFind;
    private float timerDelay = -1;
    private float delay = 3;
    // Start is called before the first frame update
    public void SetStart()
    {
        controll = transform.parent.GetComponent<Controll>();
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
      timerDelay -= Time.deltaTime;
    }

    void OnTriggerStay(Collider col){
      if (timerDelay < 0){

        if (col.tag == "Food"){
          objectFind = col.gameObject;
          timerDelay = delay;
          controll.target = objectFind;
        }

      }
    }
}
