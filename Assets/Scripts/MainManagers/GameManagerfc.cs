using UIElements;
using UnityEngine;

public class GameManagerfc : MonoBehaviour
{
    public bool Menu;
    public GameObject minionPrefab;
    public GameObject Player;
    public GameObject PlayersInformer;
    public GameObject MenuUI;
    public GameObject FinishUI;

    public int curMinionCount;
    public int minionCount;
    public int totalMinionsNew;

    public string[] leaderboard;
    public int playernum;
    private int leadernum;

    private bool gamestarted;
    private bool gamefinished;
    private bool victory;

    private void Awake()
    {
      if (PlayerPrefs.GetInt("firstStart") != 1)
      {
        PlayerPrefs.SetInt("firstStart", 1);
        PlayerPrefs.SetString("Nickname", "Player" + Random.Range(11212, 99999) );
      }

      Time.timeScale = 1;
    }

    private void Start()
    {
      QualitySettings.vSyncCount = 0;
      Application.targetFrameRate = 60;
      
      if (!Menu)
      {
        leadernum = leaderboard.Length;
        PlayersInformer.SetActive(false);
      }
    }
   
    private void Update()
    {
      if (gamestarted){
        if (curMinionCount < minionCount && totalMinionsNew < 145){
          CreateMinion(false, null, 0);
        }

        if (leadernum <= 1 && !gamefinished){
          GameFinish(true);
        }

      }

      if (gamefinished){
        if (victory){
          float ts = Time.timeScale;
          ts -= Time.unscaledDeltaTime*0.5f;
          if (ts < 0) ts = 0;
          Time.timeScale = ts;
        }
      }
    }

    public void StartGame()
    {
      foreach(GameObject g in GameObject.FindGameObjectsWithTag("Player"))
      {
        g.GetComponent<Controll>().StartGame();
      }


      while ( curMinionCount < minionCount){
        CreateMinion(true, null, 0);
      }
      minionCount += 5;


      PlayersInformer.SetActive(true);
      MenuUI.SetActive(false);

      gamestarted = true;
      GetComponent<AudioSource>().Play();
    }
    
    public void WriteDeathLeaderboard(string nick, bool bot){
      leadernum -= 1;
      leaderboard[leadernum] = "#"+ (leadernum+1) + "  " + nick;
      if (!bot) playernum = leadernum;
    }

    public void CreateMinion(bool atStart, GameObject minion, int types){ // 0 = new, 1 = old, 2 - minion = bot gameobject,
      float x = 8;
      float y = 9;
      float coef = 0.5f;
      if (!atStart) coef = 1;

      Vector3 newPos = Vector3.zero;
      bool posok = false;


      if (types == 2){
         newPos = minion.transform.position;
      }else{

         int side = Random.Range(0,4);
         if (side == 0){ //up
           x = Random.Range(-4.0f, 4.0f);
           y = Random.Range(y*2, y*2+4.1f);
         }else if (side == 1){// right
           x = Random.Range(x, x+3.0f);
           y = Random.Range(-y, y);
         }else if (side == 2){// down
           x = Random.Range(-4.0f, 4.0f);
           y = Random.Range(-y, -y-4.1f);
         }else if (side == 2){// left
           y = Random.Range(-y, y);
           x = Random.Range(-x, -x-3.0f);
         }

         if (Player){
           newPos = Player.transform.position + new Vector3(x,0,y)*coef;
         }else{
           newPos = new Vector3(0,999,0);
         }
      }

      RaycastHit hit;
      if (Physics.Raycast(newPos+Vector3.up*2.2f, -Vector3.up, out hit, 10.0f)){
        if (hit.collider.tag == "Hex") posok = true;
      }

      if (types == 2) posok = true;

      if (posok){
        if (minion == null || types == 2){
           //minion =  Instantiate(minionPrefab, Vector3.up*999, minionPrefab.transform.rotation);
           if ((curMinionCount < minionCount) || types == 2){
             minion =  Instantiate(minionPrefab, newPos, minionPrefab.transform.rotation);
             minion.GetComponent<Minion>().GM = this;
             totalMinionsNew += 1;
           }
         }

        minion.transform.position = newPos;
        minion.transform.eulerAngles = Vector3.up*Random.Range(0, 360);
        curMinionCount += 1;
        //if (types != 2) curMinionCount += 1;
      }else{
        if (minion && types != 2){
           minion.transform.position = Vector3.up*3000;
           if (types == 1){
              Destroy(minion);
              curMinionCount -= 1;
            }
         }
      }


    }

    public void DoVibro(){
      //HapticPatterns.PlayEmphasis(0.01f, 0.0f);
      Taptic.Light();
    }

    public void GetGold(int gld){
      PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold")+gld);
    }

    public void GameFinish(bool win){
      if (!gamefinished){
          if (win){
            WriteDeathLeaderboard(PlayerPrefs.GetString("Nickname"), false);
            victory = true;
          }else{
            FinishUI.GetComponent<FinishUIfc>().endTextfc.text = "TRY AGAIN";
          }

          PlayersInformer.SetActive(false);

          gamefinished = true;

          if (playernum > 2) GetGold(15);
          if (playernum == 2) GetGold(25);
          if (playernum == 1) GetGold(30);
          if (playernum == 0) GetGold(50);


          // if (metrica){
          //     string wi = "lose";
          //     if(win) wi = "win";
          //     int progress = (int)( ( (6.0f-playernum)/6.0f )*100 );
          //    metrica.level_finish(1, "01_level", 999, wi, progress, 0);
          // }
          FinishUI.SetActive(true);
      }
    }


    public string GenerateNickname(){
      string nicks = "";
      string names = "Lorder,Golum,Svego,Scratch,Minior,Scooter,TrailBoom,Bomber,Crot,Polos,CretoGard,Creamor,Scremer,Dread!!,Tormant,Grotar,trender,Porter,Potter,Mikel,Dragos,Crang,Creos,Lopas,Kayle,Toodas,Gababa,PilionLorak,Tommy,Dreamer,Josef,Joque,Kinderos,Solan,Sonar,Talos,Tanos,Ketozzzz,Dodo,Foley,Fooooty,Mikilos,Positron,Torman,Vivi,Jios,Pisko,Gurad,Jonny,Xeros,Zooomer,Zombie,velos,pooner,Spil,queqweqwe,asdas,qwee,Player35165,Player5466,Player9873,Player1264,Player7897,Player4564,Player9863,Player1216,Player1233,Player4632,Player7536,Player2678,Player1414,Player3326,Player1366,Player8422,Player9613,Player1499,Player4253,Player17655,Player5549,Player5741,Player2896,Player2137,Player9651,BloodyKnight,xAngeLx,vlom,Maels,oskar61,wanderer_from,amaze,Z1KkY,Crysler,heletch,Ч†Ю,shipilov,Chacha,usist,zingo,excurs,capitan_beans,Cashish,LUNTIK,gour,The knyazzz,American_SnIper,NIGHTMARE,007up,Dr.Dizel,RaNDoM,sportik,Su1c1de,Roger,glx506,volandband,pas,Necron,edik_lukoyanov,Synchromesh,SolomA,Dron128,DeatHSoul,DangErXeTER,Psy,Forcas,Morgot,Aspect,Kraken,Bender,Lynch,Big Papa,Mad Dog,Bowser,O’Doyle,Bruise,Psycho,Cannon,Ranger,Clink,Ratchet,Cobra,Reaper,Colt,Rigs,Crank,Ripley,Creep,Roadkill,Daemon,Ronin,Decay,Rubble,Diablo,Sasquatch,Doom,Scar,Dracula,Shiver,Dragon,Skinner,Fender,Skull Crusher,Fester,Slasher,Fisheye,Steelshot,Flack,Surge,Gargoyle,Sythe,Grave,Trip,Gunner,Trooper,Hash,Tweek,Hashtag,Vein,Indominus,Void,Ironclad,Wardon,Killer,Wraith,Knuckles,Zero,Steel,Kevlar,Lightning,Tito,Bullet-Proof,Fire-Bred,Titanium,Hurricane,Ironsides,Iron-Cut,Tempest,Iron Heart,Steel Forge,Pursuit,Steel Foil,Upsurge,Uprising,Overthrow,Breaker,Sabotage,Dissent,Subversion,Rebellion,Insurgent,Loch,Golem,Wendigo,Rex,Hydra,Behemoth,Balrog,Manticore,Gorgon,Basilisk,Minotaur,Leviathan,Cerberus,Mothman,Sylla,Charybdis,Orthros,Baal,Cyclops,Satyr,Azrael,Mariy_Kis,KATUSHA,KinDer,Eva,BoSoranY,AlfabetkA,ANGEL,Äђģę";
      int rnd = Random.Range(0, 5);
      string[] selectedName = names.Split(char.Parse(","));

      if (rnd == 0)
      {
        rnd = Random.Range(1131, 98340);
        nicks = "Player"+rnd;
      }
      else
      {
        nicks = selectedName[Random.Range(0, selectedName.Length)];
      }

      return nicks;
    }
}
