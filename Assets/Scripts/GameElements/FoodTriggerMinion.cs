using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTriggerMinion : MonoBehaviour
{



    void OnTriggerEnter(Collider col){
      if (col.tag == "Player"){
        col.GetComponent<Controll>().MinionGet(gameObject);
      }
      if (col.tag == "Minion"){
        col.GetComponent<Minion>().player.GetComponent<Controll>().MinionGet(gameObject);
      }

    }
}
