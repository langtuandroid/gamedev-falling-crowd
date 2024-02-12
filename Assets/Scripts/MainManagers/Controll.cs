using UnityEngine;
using CnControls;
using MainManagers;
using Other;
using TMPro;

public class Controll : MonoBehaviour
{
    public bool Bot;
    private GameManagerfc GM;
    public bool controllerStopMove;
    public int count;
      private int tempCount;
    public string nickname;
    [Space]
    public float speed;
    [Space]
    public bool visible;
    public bool notShowArrows;
    [Space]
    public Color playerColor;
    public Transform FinderCollider;
    public Transform joy;
    public Transform botJoy;
    public SkinnedMeshRenderer smr;
    public Transform PlayerUI;
    public TextMeshPro t_count;
    public TextMeshPro t_nickname;
    //public TextMesh t_nicknamesdw;
    public GameObject target;
    public SpriteRenderer score_box;
    public bool DoUiArrow;
    public GameObject UIarrowEnemy;
    public GameObject SimpleArrowEnemy;

    private bool started;
    private Vector3 camPos;
    private Camera camera;
    private Animator t_count_animator;
    private float rotateSmooth = 15.5f;
    private int turnside;
    private float delayTargetLook;
    private float timerNoFloor;
    private float timerMoveTurn;
    private float timerMinionGet;
    private float coefFalling = 1;

    public bool gamestarted;
    private AudioSource AS;

    private float timerFall;
    private bool falling;

    void Start(){
              PlayerUI.transform.position = Vector3.up*999;
    }
    // Start is called before the first frame update
    void SetStart()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManagerfc>();
        timerFall = -1;
        PlayerUI.parent = null;

        t_count.text = ""+ count;
        smr.gameObject.transform.parent.transform.GetComponent<Animator>().enabled = true;

        if (Bot){
          //gameObject.AddComponent<MeshRenderer>();
          if (!notShowArrows){
            if (DoUiArrow){
              UIarrowEnemy = Instantiate(UIarrowEnemy, GameObject.Find("Canvas").transform);
              UIarrowEnemy.GetComponent<ArrowToEnemy>().SetStart(gameObject, playerColor);
            }else{
              SimpleArrowEnemy = Instantiate(SimpleArrowEnemy, GM.Playerfc.GetComponent<Controll>().PlayerUI.transform);
              Color col = playerColor;
              col.a = 0.5f;
              SimpleArrowEnemy.GetComponent<ArrowToEnemySimple>().SetStart(gameObject, col );
            }
          }
          gameObject.AddComponent<CheckVisible>().SetStart(this);
          //smr.gameObject.AddComponent<CheckVisible>().SetStart(this);
          nickname = GM.GenerateNicknamefc();
          FinderCollider.GetComponent<FinderCollider>().SetStart();
          rotateSmooth = 17.5f;
          turnside = Random.Range (-1, 1);
          if (turnside == 0) turnside = 1;
        }else{
          AS = GetComponent<AudioSource>();
          Destroy(FinderCollider.gameObject);
          nickname = PlayerPrefs.GetString("Nickname");
          t_count_animator = t_count.GetComponent<Animator>();
          camera = Camera.main;
          camPos = camera.transform.position- transform.position;
        }

        t_nickname.text = nickname;
        //t_nicknamesdw.text = nickname;
        smr.material.color = playerColor;
        score_box.color = playerColor;

    }

    public void StartGame(){
      SetStart();
      gamestarted = true;
    }
    // Update is called once per frame
    void Update()
    {
      if (gamestarted)
      {
        timerFall += Time.deltaTime;
          /////// CHECK FALLING AND RAYCAST/////////////////////////////////
          RaycastHit hit;
          if (Physics.Raycast(transform.position-Vector3.up*3, Vector3.up, out hit, 5, (1 << 6) )){
            var col = hit.collider;

            if (col.tag == "Hex"){
              col.gameObject.AddComponent<OnHex>();
              col.gameObject.tag = "HexOn";
              timerFall = 0;
            }
            if (col.tag == "HexOn"){
              timerFall = 0;
            }
          }
          //////////////////////////////////////////////////////////////////

          if (tempCount != count){
            if (count < 1) count = 1;
            if (tempCount < count) tempCount += 1;
            if (tempCount > count) tempCount -= 1;
            t_count.text = ""+ count;
            if (!Bot) t_count_animator.Play("t_count",0,0);
          }


          PlayerUI.position = transform.position;

          if (!Bot)
          {
              if (!controllerStopMove) transform.Translate(Vector3.forward * speed * Time.deltaTime);

              if (Input.GetMouseButton(0)){
                if (controllerStopMove)  transform.Translate(Vector3.forward * speed * Time.deltaTime);
                  if (CnInputManager.GetAxis("Horizontal") != 0 || CnInputManager.GetAxis("Vertical") != 0)
                  {
                    MovePlayer(new Vector2(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"))*100);
                  }
              }
              /*
              if (CnInputManager.GetAxis("Jump") > 0){
                jump true
              }else{
                jump false
              }*/

              camera.transform.position = transform.position + camPos;//new Vector3(transform.position.x,10,transform.position.z-10);
              camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 60+count*0.1f, Time.deltaTime*2);
          }
          else
          {
            //////// *****************   BOT   ******************* ///////// *********************** //////////
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

              FinderCollider.position = transform.position;
              delayTargetLook -= Time.deltaTime;
              timerMoveTurn -= Time.deltaTime;

              MovePlayer(new Vector2(joy.position.x - FinderCollider.position.x, joy.position.z - FinderCollider.position.z) );

              if (delayTargetLook < 0){
                if (target){
                  botJoy.LookAt(target.transform);
                }else if (timerMoveTurn < 0){
                  timerMoveTurn = Random.Range(2.1f, 6.7f);
                  botJoy.eulerAngles += Vector3.up*Random.Range(-60 ,60);
                }
              }

              if (!Physics.Raycast(joy.position+(Vector3.up*2) , -Vector3.up, out hit, 3)){
                timerNoFloor += Time.deltaTime;
                if (timerNoFloor > 0.15f){
                  float angle = 30;

                  botJoy.eulerAngles += Vector3.up*angle*turnside;
                  if (!Physics.Raycast(joy.position+(Vector3.up*2) , -Vector3.up, out hit, 3)){
                      botJoy.eulerAngles -= Vector3.up*(2*angle)*turnside;
                  }
                  delayTargetLook = 3;
                  //botJoy.eulerAngles += Vector3.up*angle*turnside;
                  //timerNoFloor = 0;
                }
              }else{
                if (timerNoFloor > 0){
                //  turnside = Random.Range (-1, 1);
                //  if (turnside == 0) turnside = 1;
                }
                timerNoFloor = 0;
              }

              if (!visible){
                timerMinionGet += Time.deltaTime;
                if (timerMinionGet > 1.5f){
                  timerMinionGet = 0;
                  GM.CreateMinionfc(false, gameObject, 2);
                }
                coefFalling = 0.009f;
              }else{
                coefFalling = 1;
              }

            //////// ****************   END BOT   **************** ///////// *********************** //////////
          }


          var posy = transform.position.y;

          if (falling){
            if (posy > 0){
               transform.position = new Vector3(transform.position.x, 0, transform.position.z);
               falling = false;
             }
          }


          if (timerFall > 0.05f)
          {
            if (!started ){ started = true; timerFall = 0;}
            falling = true;
            //transform.Translate(Vector3.forward * speed * 0.8f * Time.deltaTime * coefFalling);
            transform.Translate(-Vector3.up * 15 * Time.deltaTime * coefFalling);
          }
          else
          {
            if (posy < 0 && posy > -0.4f){
               transform.Translate(Vector3.up * 1 * Time.deltaTime);
            }else{
              if (falling){
                transform.Translate(-Vector3.up * 4 * Time.deltaTime* coefFalling);
              }
            }
          }

          if (posy < -2) Death();
      }
    }

    public void Death(){
      GM.WriteDeathLeaderboardfc(nickname, Bot);
      Destroy(this);
      if (Bot){
        //Destroy(arrowEnemy);
        Destroy(FinderCollider.gameObject);
      }else{
        GM.GameFinishfc(false);
      }

      Destroy(PlayerUI.gameObject);

      Destroy(gameObject);
    }

    public void MovePlayer( Vector2 move){

                float x = move.x;
                float y = move.y;
                float coef = 1/x;
                if (x > 1 || x < -1){
                  x = x*Mathf.Abs(coef);
                  y = y*Mathf.Abs(coef);

                }
                if (y > 1 || y < -1){
                  coef = 1/y;
                  x = x*Mathf.Abs(coef);
                  y = y*Mathf.Abs(coef);
                }



                move = new Vector2(x,y);
              //  if (Mathf.Abs(x) > Mathf.Abs(y)) animator.speed = Mathf.Abs(x);
              //  if (Mathf.Abs(y) > Mathf.Abs(x)) animator.speed = Mathf.Abs(y);

          //rb.velocity = new Vector3(move.x * speed * Time.deltaTime * 10, rb.velocity.y, move.y * speed * Time.deltaTime * 10);
          float angle = Mathf.Atan2(move.x, move.y) * Mathf.Rad2Deg;
          float CurAngle = 0;

             CurAngle = Mathf.LerpAngle(transform.eulerAngles.y, angle, Time.deltaTime*rotateSmooth);

          transform.eulerAngles = new Vector3(0, CurAngle, 0);

    }

    void OnTriggerStay(Collider col){
      if (started){
          if (col.tag == "Food"){
            MinionGet(col.gameObject);
          }
          /*
          if (col.tag == "Hex"){
            col.gameObject.AddComponent<OnHex>();
            timerFall = 0;
          }
          if (col.tag == "HexOn"){
            timerFall = 0;
          }*/
      }
    }

    void OnCollisionEnter(Collision col){
          if (col.collider.tag == "Minion"){
            Minion minion = col.collider.gameObject.GetComponent<Minion>();
            if (minion.player != transform){
              if (minion.controll.count < count){
                minion.controll.count -= 1;
                minion.SetMinion(transform, speed, playerColor, this);
                gameObject.AddComponent<ScaleZeroPosfc>();
                minion.controll.SoundMinion();
                  count += 1;
              }
            }
          }else if (col.collider.tag == "Player"){
            Controll controllpl = col.collider.gameObject.GetComponent<Controll>();
            if (controllpl.count < count){
              if (controllpl.count < 2){
                controllpl.Death();
                SoundMinion();
              }
            }
          }
    }

    public void MinionGet(GameObject minion){
      count += 1;
      minion.GetComponent<Minion>().SetMinion(transform, speed, playerColor, this);
      minion.GetComponent<Minion>().enabled = true;
      minion.AddComponent<ScaleZeroPosfc>();

      if (Bot) target = null;
      SoundMinion();

    }

    public void SoundMinion(){
      if (!Bot){
        AS.pitch = Random.Range(0.89f, 1.11f);
        AS.Play();
      }
    }
}
