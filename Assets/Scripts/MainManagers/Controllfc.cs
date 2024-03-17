using CnControls;
using GameElements;
using Other;
using TMPro;
using UnityEngine;
using Zenject;

namespace MainManagers
{
  public class Controllfc : MonoBehaviour
  {
    [SerializeField]
    private CharacterData _characterData;
    public bool Botfc;
    public bool controllerStopMovefc;
    public int countfc;
    private int tempCountfc;
    public string nicknamefc;
    public float speedfc;
    public bool visiblefc;
    public bool notShowArrowsfc;
    [Space]
    public Color playerColorfc;
    public Transform FinderColliderfc;
    public Transform joyfc;
    public Transform botJoyfc;
    public SkinnedMeshRenderer skinmeshfc;
    public Transform PlayerUIfc;
    public TextMeshPro t_countfc;
    public TextMeshPro t_nicknamefc;
    public GameObject targetfc;
    public SpriteRenderer score_boxfc;
    public bool DoUiArrowfc;
    public GameObject UIarrowEnemyfc;
    public GameObject SimpleArrowEnemyfc;

    private bool startedfc;
    private Vector3 camPosfc;
    private Camera camerafc;
    private Animator t_count_animatorfc;
    private float rotateSmoothfc = 15.5f;
    private int turnsidefc;
    private float delayTargetLookfc;
    private float timerNoFloorfc;
    private float timerMoveTurnfc;
    private float timerMinionGetfc;
    private float coefFallingfc = 1;

    public bool gamestartedfc;
    private AudioSource AudioSourcefc;

    private float timerFallfc;
    private bool fallingfc;

    private GameManagerfc _gameManagerfc;

    [Inject]
    private void Context(GameManagerfc gameManagerfc)
    {
      _gameManagerfc = gameManagerfc;
    }

    private void Start()
    {
      PlayerUIfc.transform.position = Vector3.up * 999;
    }
    
    public void InitCharacterfc()
    {
      if (Botfc)
      {
        LoadData();
      }
      SetStartfc();
      gamestartedfc = true;
    }

    private void LoadData()
    {
      speedfc = _characterData.Speed;
      playerColorfc = _characterData.Color;
    }
    
    private void SetStartfc()
    {
      //_gameManagerfc = GameObject.Find("GameManager").GetComponent<GameManagerfc>();
      timerFallfc = -1;
      PlayerUIfc.parent = null;

      t_countfc.text = ""+ countfc;
      skinmeshfc.gameObject.transform.parent.transform.GetComponent<Animator>().enabled = true;

      if (Botfc)
      {
        //gameObject.AddComponent<MeshRenderer>();
        if (!notShowArrowsfc)
        {
          if (DoUiArrowfc)
          {
            UIarrowEnemyfc = Instantiate(UIarrowEnemyfc, GameObject.Find("Canvas").transform);
            UIarrowEnemyfc.GetComponent<ArrowToEnemyfc>().SetStart(gameObject, playerColorfc);
          }
          else
          {
            SimpleArrowEnemyfc = Instantiate(SimpleArrowEnemyfc, _gameManagerfc.PrefabsPlayer.GetComponent<Controllfc>().PlayerUIfc.transform);
            Color col = playerColorfc;
            col.a = 0.5f;
            SimpleArrowEnemyfc.GetComponent<ArrowToEnemySimplefc>().SetStart(gameObject, col );
          }
        }
        gameObject.AddComponent<CheckVisiblefc>().SetStart(this);
        //smr.gameObject.AddComponent<CheckVisible>().SetStart(this);
        nicknamefc = _gameManagerfc.GenerateNicknamefc();
        FinderColliderfc.GetComponent<FinderColliderfc>().SetStart();
        rotateSmoothfc = 17.5f;
        turnsidefc = Random.Range (-1, 1);
        if (turnsidefc == 0) turnsidefc = 1;
      }
      else
      {
        AudioSourcefc = GetComponent<AudioSource>();
        Destroy(FinderColliderfc.gameObject);
        nicknamefc = PlayerPrefs.GetString("Nickname");
        t_count_animatorfc = t_countfc.GetComponent<Animator>();
        camerafc = Camera.main;
        camPosfc = camerafc.transform.position- transform.position;
      }

      t_nicknamefc.text = nicknamefc;
      //t_nicknamesdw.text = nickname;
      //skinmeshfc.material.color = playerColorfc;
      score_boxfc.color = playerColorfc;

    }

    private void Update()
    {
      if (gamestartedfc)
      {
        timerFallfc += Time.deltaTime;
        /////// CHECK FALLING AND RAYCAST/////////////////////////////////
        RaycastHit hit;
        if (Physics.Raycast(transform.position-Vector3.up*3, Vector3.up, out hit, 5, (1 << 6) )){
          var col = hit.collider;

          if (col.tag == "Hex"){
            col.gameObject.AddComponent<OnHexfc>();
            col.gameObject.tag = "HexOn";
            timerFallfc = 0;
          }
          if (col.tag == "HexOn"){
            timerFallfc = 0;
          }
        }
        //////////////////////////////////////////////////////////////////

        if (tempCountfc != countfc){
          if (countfc < 1) countfc = 1;
          if (tempCountfc < countfc) tempCountfc += 1;
          if (tempCountfc > countfc) tempCountfc -= 1;
          t_countfc.text = ""+ countfc;
          if (!Botfc) t_count_animatorfc.Play("t_count",0,0);
        }


        PlayerUIfc.position = transform.position;

        if (!Botfc)
        {
          if (!controllerStopMovefc) transform.Translate(Vector3.forward * speedfc * Time.deltaTime);

          if (Input.GetMouseButton(0)){
            if (controllerStopMovefc)  transform.Translate(Vector3.forward * speedfc * Time.deltaTime);
            if (CnInputManager.GetAxis("Horizontal") != 0 || CnInputManager.GetAxis("Vertical") != 0)
            {
              MovePlayerfc(new Vector2(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"))*100);
            }
          }
          /*
              if (CnInputManager.GetAxis("Jump") > 0){
                jump true
              }else{
                jump false
              }*/

          camerafc.transform.position = transform.position + camPosfc;//new Vector3(transform.position.x,10,transform.position.z-10);
          camerafc.fieldOfView = Mathf.Lerp(camerafc.fieldOfView, 60+countfc*0.1f, Time.deltaTime*2);
        }
        else
        {
          //////// *****************   BOT   ******************* ///////// *********************** //////////
          transform.Translate(Vector3.forward * speedfc * Time.deltaTime);

          FinderColliderfc.position = transform.position;
          delayTargetLookfc -= Time.deltaTime;
          timerMoveTurnfc -= Time.deltaTime;

          MovePlayerfc(new Vector2(joyfc.position.x - FinderColliderfc.position.x, joyfc.position.z - FinderColliderfc.position.z) );

          if (delayTargetLookfc < 0){
            if (targetfc){
              botJoyfc.LookAt(targetfc.transform);
            }else if (timerMoveTurnfc < 0){
              timerMoveTurnfc = Random.Range(2.1f, 6.7f);
              botJoyfc.eulerAngles += Vector3.up*Random.Range(-60 ,60);
            }
          }

          if (!Physics.Raycast(joyfc.position+(Vector3.up*2) , -Vector3.up, out hit, 3)){
            timerNoFloorfc += Time.deltaTime;
            if (timerNoFloorfc > 0.15f){
              float angle = 30;

              botJoyfc.eulerAngles += Vector3.up*angle*turnsidefc;
              if (!Physics.Raycast(joyfc.position+(Vector3.up*2) , -Vector3.up, out hit, 3)){
                botJoyfc.eulerAngles -= Vector3.up*(2*angle)*turnsidefc;
              }
              delayTargetLookfc = 3;
              //botJoy.eulerAngles += Vector3.up*angle*turnside;
              //timerNoFloor = 0;
            }
          }else{
            if (timerNoFloorfc > 0){
              //  turnside = Random.Range (-1, 1);
              //  if (turnside == 0) turnside = 1;
            }
            timerNoFloorfc = 0;
          }

          if (!visiblefc){
            timerMinionGetfc += Time.deltaTime;
            if (timerMinionGetfc > 1.5f){
              timerMinionGetfc = 0;
              _gameManagerfc.CreateMinionfc(false, gameObject, 2);
            }
            coefFallingfc = 0.009f;
          }else{
            coefFallingfc = 1;
          }

          //////// ****************   END BOT   **************** ///////// *********************** //////////
        }


        var posy = transform.position.y;

        if (fallingfc){
          if (posy > 0){
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            fallingfc = false;
          }
        }


        if (timerFallfc > 0.05f)
        {
          if (!startedfc ){ startedfc = true; timerFallfc = 0;}
          fallingfc = true;
          //transform.Translate(Vector3.forward * speed * 0.8f * Time.deltaTime * coefFalling);
          transform.Translate(-Vector3.up * 15 * Time.deltaTime * coefFallingfc);
        }
        else
        {
          if (posy < 0 && posy > -0.4f){
            transform.Translate(Vector3.up * 1 * Time.deltaTime);
          }else{
            if (fallingfc){
              transform.Translate(-Vector3.up * 4 * Time.deltaTime* coefFallingfc);
            }
          }
        }

        if (posy < -2) Deathfc();
      }
    }

    public void Deathfc()
    {
      _gameManagerfc.WriteDeathLeaderboardfc(nicknamefc, Botfc);
      Destroy(this);
      if (Botfc)
      {
        //Destroy(arrowEnemy);
        Destroy(FinderColliderfc.gameObject);
      }
      else
      {
        _gameManagerfc.GameFinishfc(false);
      }

      Destroy(PlayerUIfc.gameObject);

      Destroy(gameObject);
    }

    public void MovePlayerfc( Vector2 move){

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
      CurAngle = Mathf.LerpAngle(transform.eulerAngles.y, angle, Time.deltaTime*rotateSmoothfc);
      transform.eulerAngles = new Vector3(0, CurAngle, 0);
    }

    private void OnTriggerStay(Collider col)
    {
      if (startedfc){
        if (col.CompareTag("Food")){
          MinionGetfc(col.gameObject);
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

    private void OnCollisionEnter(Collision col)
    {
      if (col.collider.CompareTag("Minion"))
      {
        Minionfc minionfc = col.collider.gameObject.GetComponent<Minionfc>();
        if (minionfc.playerfc != transform){
          if (minionfc.controllfc.countfc < countfc){
            minionfc.controllfc.countfc -= 1;
            minionfc.SetMinionfc(transform, speedfc, playerColorfc, this);
            gameObject.AddComponent<ScaleZeroPosfc>();
            minionfc.controllfc.SoundMinionfc();
            countfc += 1;
          }
        }
      }
      else if (col.collider.CompareTag("Player"))
      {
        Controllfc controllpl = col.collider.gameObject.GetComponent<Controllfc>();
        if (controllpl.countfc < countfc){
          if (controllpl.countfc < 2){
            controllpl.Deathfc();
            SoundMinionfc();
          }
        }
      }
    }

    public void MinionGetfc(GameObject minion)
    {
      countfc += 1;
      minion.GetComponent<Minionfc>().SetMinionfc(transform, speedfc, playerColorfc, this);
      minion.GetComponent<Minionfc>().enabled = true;
      minion.AddComponent<ScaleZeroPosfc>();

      if (Botfc) targetfc = null;
      SoundMinionfc();

    }

    public void SoundMinionfc()
    {
      if (!Botfc){
        AudioSourcefc.pitch = Random.Range(0.89f, 1.11f);
        AudioSourcefc.Play();
      }
    }
  }
}
