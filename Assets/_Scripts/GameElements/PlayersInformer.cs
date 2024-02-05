using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersInformer : MonoBehaviour
{

    public GameObject[] places;
    public Transform layers;

    private float timer;
    private int num;

    void Update()
    {
      timer += Time.deltaTime;

      if (timer > 0.5f){
        timer = 0;
        num = 0;
        foreach(Transform t in layers){
             t.position = places[num].transform.position;
             num += 1;
        }
      }

    }
}
