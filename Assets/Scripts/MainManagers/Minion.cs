using System.Collections;
using System.Collections.Generic;
using GameElements;
using MainManagers;
using Other;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public Transform player;
    public bool doDelayLookAt;
    public float speed;
    private float delay;
    private float timer;
    private float timerRun;
    public SkinnedMeshRenderer smr;
    public GameManagerfc GM;
    private float timerDist;

    public Controll controll;
    private float timerresp;
    private float dist;
    private float timerFall;
    private bool falling;

    // Start is called before the first frame update
    public void SetMinion(Transform p, float s, Color c, Controll ctrl)
    {
      tag = "Minion";
      player = p;
      speed = s;
      controll = ctrl;
      GetComponent<SphereCollider>().isTrigger = false;
      if (doDelayLookAt){
         delay = Random.Range(0.15f, 0.4f);
      }
      //Destroy(GetComponent<FoodTriggerMinion>());
      timerRun = 2;
      smr.material.color = c;
      GM.curMinionCountfc -= 1;
      smr.gameObject.transform.parent.transform.GetComponent<Animator>().enabled = true;
      timerDist = Random.Range(0.01f, 0.39f);
    }

    // Update is called once per frame
    void Update()
    {
        timerDist += Time.deltaTime;
        timerFall += Time.deltaTime;

        if (player){
          /////// CHECK FALLING AND RAYCAST/////////////////////////////////

          RaycastHit hit;
          if (Physics.Raycast(transform.position-Vector3.up*3, Vector3.up, out hit, 5, (1 << 6))){
            var col = hit.collider;

            if (col.tag == "Hex"){
              col.gameObject.AddComponent<OnHexfc>();
              col.gameObject.tag = "HexOn";
              timerFall = 0;
            }
            if (col.tag == "HexOn"){
              timerFall = 0;
            }
          }

          //////////////////////////////////////////////////////////////////
          timer -= Time.deltaTime;
          if (doDelayLookAt){
            if (timer < 0){
              transform.LookAt(player);
            }
          }else{
            transform.LookAt(player);
            //transform.eulerAngles = player.eulerAngles;
          }

          if (timer < 0){
              timer = delay;
            //  CheckWater();
          }
          if (timerDist > 0.4f) dist = Vector3.Distance(transform.position,  player.position);
            //if (!falling) transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime*speed*dist*0.5f);
            if (!falling) transform.Translate(Vector3.forward * Time.deltaTime * speed * dist *0.3f);
            //if (!falling) GetComponent<Rigidbody>().MovePosition(transform.forward * speed * Time.deltaTime);
        }/*else{
          if (timerDist > 3){
            if (GM.Player){
              if (Vector3.Distance(transform.position, GM.Player.transform.position) > 18) GM.CreateMinion(false, gameObject, 1);
            }
          }
        }*/


        var posy = transform.position.y;

        if (falling){
          if (posy > 0){
             transform.position = new Vector3(transform.position.x, 0, transform.position.z);
             falling = false;
           }
        }
        if (timerFall > 0.19f){
          falling = true;
          transform.Translate(Vector3.forward * speed * 0.4f * Time.deltaTime);
          transform.position -= Vector3.up * 5 * Time.deltaTime;
          //transform.Translate(-Vector3.up * 5 * Time.deltaTime);
        }else{
          if (posy < 0 && posy > -0.4f){
             //transform.Translate(Vector3.up * 1 * Time.deltaTime);
             transform.position += Vector3.up * 1 * Time.deltaTime;
          }else{
            if (falling){
              transform.position -= Vector3.up * 4 * Time.deltaTime;
              //transform.Translate(-Vector3.up * 4 * Time.deltaTime);
            }
          }
        }

        if (posy < -0.94f) ReleaseMinion();

        /*
        if (posy > 1000){
           if (GM.Player){
             timerresp -= Time.deltaTime;
              if (timerresp < 0){
                GM.CreateMinion(false, gameObject, 1);
                timerresp = Random.Range(1.1f, 10.1f);
              }
            }
         }*/
    }

    public void ReleaseMinion(){
      GetComponent<SphereCollider>().isTrigger = true;
      smr.material.color = new Color(0.75f,0.75f,0.75f);

      if (controll){
         controll.count -= 1;
         controll = null;
       }
      player = null;
      GM.CreateMinionfc(false, gameObject, 1);
      falling = false;
      this.enabled = false;
      tag = "Food";
      smr.gameObject.transform.parent.transform.GetComponent<Animator>().enabled = false;
    }

    void OnTriggerStay(Collider col){
      if (!player){
          if (col.tag == "Player"){
            col.GetComponent<Controll>().MinionGet(gameObject);
          }
          if (col.tag == "Minion"){
            col.GetComponent<Minion>().controll.MinionGet(gameObject);
          }
      }
    }

    void OnCollisionEnter(Collision col){
          if (col.collider.tag == "Minion"){
            Minion minion = col.collider.gameObject.GetComponent<Minion>();
            if (minion.player != player){
              if (minion.controll.count > controll.count){
                controll.count -= 1;
                SetMinion(minion.player, minion.controll.speed, minion.controll.playerColor, minion.controll);
                gameObject.AddComponent<ScaleZeroPosfc>();
                minion.controll.SoundMinion();
                  minion.controll.count += 1;
              }
            }
          }else if (col.collider.tag == "Player"){
            Controll controllpl = col.collider.gameObject.GetComponent<Controll>();
            if (controllpl.count < controll.count){
              if (controllpl.count < 2){
                col.collider.gameObject.GetComponent<Controll>().Death();
                controll.SoundMinion();
              }
            }
          }
    }
}
