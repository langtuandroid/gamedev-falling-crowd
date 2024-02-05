using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowToEnemySimple : MonoBehaviour
{
    public GameObject Enemy;
    private float timer;
    public bool started = true;
    public SpriteRenderer img;
    // Start is called before the first frame update
    public void SetStart(GameObject g, Color col)
    {
      Enemy = g;
      started = true;
      img.color = col;
      //timer = Random.Range(0.01f, 0.43f);
    }

    // Update is called once per frame
    void Update()
    {
      /*timer -= Time.deltaTime;
      if (timer < 0){
        timer = 0.2f;*/
        if (started){
          if (Enemy){
            if (Vector3.Distance(transform.position, Enemy.transform.position) > 5){
              transform.LookAt(Enemy.transform);
            }else{
              transform.eulerAngles = new Vector3(90,0,0);
            }
          }else{
            Destroy(gameObject);
          }
        }

    }
}
