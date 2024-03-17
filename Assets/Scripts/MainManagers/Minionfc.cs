using GameElements;
using Other;
using UnityEngine;

namespace MainManagers
{
  public class Minionfc : MonoBehaviour
  {
    public GameManagerfc GameMangerfc;
    
    public Transform playerfc;
    public bool doDelayLookAtfc;
    public float speedfc;
    private float delayfc;
    private float timerfc;
    private float timerRunfc;
    public SkinnedMeshRenderer smrfc;
   
    private float timerDistfc;

    public Controllfc controllfc;
    private float timerrespfc;
    private float distfc;
    private float timerFallfc;
    private bool fallingfc;

    
    public void SetMinionfc(Transform position, float speed, Color color, Controllfc controller)
    {
      tag = "Minion";
      playerfc = position;
      speedfc = speed;
      controllfc = controller;
      GetComponent<SphereCollider>().isTrigger = false;
      if (doDelayLookAtfc)
      {
        delayfc = Random.Range(0.15f, 0.4f);
      }
      //Destroy(GetComponent<FoodTriggerMinion>());
      timerRunfc = 2;
      smrfc.material.color = color;
      GameMangerfc.curMinionCountfc -= 1;
      smrfc.gameObject.transform.parent.transform.GetComponent<Animator>().enabled = true;
      timerDistfc = 0.3f; //Random.Range(0.01f, 0.39f);
    }

    
    private void Update()
    {
      timerDistfc += Time.deltaTime;
      timerFallfc += Time.deltaTime;

      if (playerfc)
      {
        /////// CHECK FALLING AND RAYCAST/////////////////////////////////
        RaycastHit hit;
        if (Physics.Raycast(transform.position-Vector3.up*3, Vector3.up, out hit, 5, (1 << 6))){
          var col = hit.collider;

          if (col.CompareTag("Hex"))
          {
            col.gameObject.AddComponent<OnHexfc>();
            col.gameObject.tag = "HexOn";
            timerFallfc = 0;
          }
          if (col.CompareTag("HexOn"))
          {
            timerFallfc = 0;
          }
        }
        //////////////////////////////////////////////////////////////////
        timerfc -= Time.deltaTime;
        if (doDelayLookAtfc)
        {
          if (timerfc < 0)
          {
            transform.LookAt(playerfc);
          }
        }
        else
        {
          transform.LookAt(playerfc);
          //transform.eulerAngles = playerfc.eulerAngles;
        }

        if (timerfc < 0)
        {
          timerfc = delayfc;
        }
        if (timerDistfc > 0.4f) distfc = Vector3.Distance(transform.position,  playerfc.position);
        //if (!falling) transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime*speed*dist*0.5f);
        if (!fallingfc) transform.Translate(Vector3.forward * Time.deltaTime * speedfc * distfc *0.3f);
      }


      var posy = transform.position.y;

      if (fallingfc)
      {
        if (posy > 0)
        {
          transform.position = new Vector3(transform.position.x, 0, transform.position.z);
          fallingfc = false;
        }
      }
      if (timerFallfc > 0.19f)
      {
        fallingfc = true;
        transform.Translate(Vector3.forward * speedfc * 0.4f * Time.deltaTime);
        transform.position -= Vector3.up * 5 * Time.deltaTime;
        //transform.Translate(-Vector3.up * 5 * Time.deltaTime);
      }
      else
      {
        if (posy < 0 && posy > -0.4f){
          //transform.Translate(Vector3.up * 1 * Time.deltaTime);
          transform.position += Vector3.up * 1 * Time.deltaTime;
        }else{
          if (fallingfc){
            transform.position -= Vector3.up * 4 * Time.deltaTime;
            //transform.Translate(-Vector3.up * 4 * Time.deltaTime);
          }
        }
      }

      if (posy < -0.94f) ReleaseMinionfc();

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

    private void ReleaseMinionfc()
    {
      GetComponent<SphereCollider>().isTrigger = true;
      smrfc.material.color = new Color(0.75f,0.75f,0.75f);

      if (controllfc){
        controllfc.countfc -= 1;
        controllfc = null;
      }
      playerfc = null;
      GameMangerfc.CreateMinionfc(false, gameObject, 1);
      fallingfc = false;
      this.enabled = false;
      tag = "Food";
      smrfc.gameObject.transform.parent.transform.GetComponent<Animator>().enabled = false;
    }

    private void OnTriggerStay(Collider col)
    {
      if (!playerfc){
        if (col.CompareTag("Player")){
          col.GetComponent<Controllfc>().MinionGetfc(gameObject);
        }
        if (col.CompareTag("Minion")){
          col.GetComponent<Minionfc>().controllfc.MinionGetfc(gameObject);
        }
      }
    }

    private void OnCollisionEnter(Collision col)
    {
      if (col.collider.CompareTag("Minion"))
      {
        Minionfc minionfc = col.collider.gameObject.GetComponent<Minionfc>();
        if (minionfc.playerfc != playerfc)
        {
          if (minionfc.controllfc.countfc > controllfc.countfc)
          {
            controllfc.countfc -= 1;
            SetMinionfc(minionfc.playerfc, minionfc.controllfc.speedfc, minionfc.controllfc.playerColorfc, minionfc.controllfc);
            gameObject.AddComponent<ScaleZeroPosfc>();
            minionfc.controllfc.SoundMinionfc();
            minionfc.controllfc.countfc += 1;
          }
        }
      }
      else if (col.collider.CompareTag("Player"))
      {
        Controllfc controllpl = col.collider.gameObject.GetComponent<Controllfc>();
        if (controllpl.countfc < controllfc.countfc)
        {
          if (controllpl.countfc < 2)
          {
            col.collider.gameObject.GetComponent<Controllfc>().Deathfc();
            controllfc.SoundMinionfc();
          }
        }
      }
    }
  }
}
