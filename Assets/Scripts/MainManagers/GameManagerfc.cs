using System.Collections.Generic;
using GameElements;
using Integration;
using UIElements;
using UnityEngine;
using Zenject;

namespace MainManagers
{
  public class GameManagerfc : MonoBehaviour
  {
    [SerializeField] 
    private List<Transform> _spawnPointEnemy;
    [SerializeField] 
    private Transform _spawnPointPlayer;
    [SerializeField] 
    private Transform _spawnHolderEnemy;
    [SerializeField] 
    private List<GameObject> _prefabsEnemy;
    [SerializeField] 
    private List<OnLayerfc> _layerfc;
    
    public bool Menufc;
    public GameObject minionPrefabfc;
   // public GameObject Playerfc;
    public GameObject PlayersInformerfc;
    public GameObject MenuUIfc;
    public GameObject GameMenuUIfc;
    public GameObject FinishUIfc;
    public int curMinionCountfc;
    public int minionCountfc;
    public int totalMinionsNewfc;
    public string[] leaderboardfc;
    public int playernumfc;
    private int leadernumfc;
    private bool gamestartedfc;
    private bool gamefinishedfc;
    private bool victoryfc;
    
    private const string IntegrationsCounter = "IntegrationsCounter";
    private int loadLevelCount = 0; 

    public GameObject _prefabsPlayerNew;
    public GameObject PrefabsPlayer => _prefabsPlayerNew;
    
    private IAPService _iapService;
    private AdMobController _adMobController;

    [Inject]
    private void Construct(IAPService iapService, AdMobController adMobController)
    {
      _iapService = iapService;
      _adMobController = adMobController;
    }
    
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
      //Application.targetFrameRate = 60;
      CreateCharacters();
      if (!Menufc)
      {
        leadernumfc = leaderboardfc.Length;
        PlayersInformerfc.SetActive(false);
      }
      
      ShowIntegration();
    }
    
    private void ShowIntegration()
    {
      loadLevelCount = PlayerPrefs.GetInt(IntegrationsCounter, 0);
      loadLevelCount++;
      _adMobController.ShowBanner(true);
      if (loadLevelCount % 2 == 0)
      {
        _adMobController.ShowInterstitialAd();
      }
      else if (loadLevelCount % 3 == 0)
      {
        _iapService.ShowSubscriptionPanel();
      }

      if (loadLevelCount >= 3)
      {
        loadLevelCount = 0;
      }
      PlayerPrefs.SetInt(IntegrationsCounter, loadLevelCount);
      PlayerPrefs.Save(); 
    }

    private void CreateCharacters()
    {
      int index = SaveLoadManager.LoadChoseCharacter();
      Debug.Log(" Index= " + index);
      _prefabsPlayerNew = Instantiate(_prefabsEnemy[index], _spawnPointPlayer.position, Quaternion.identity, _spawnPointPlayer.transform);
      var characterController = _prefabsPlayerNew.GetComponent<Controllfc>();
      characterController.Botfc = false;
      characterController.speedfc = 5f;
      //_layerfc[0].SetPlayercOntroller(characterController);
      
      for (int i = 0; i < _prefabsEnemy.Count; i++)
      {
        var newEnemy = Instantiate(_prefabsEnemy[i], _spawnPointEnemy[i].position, Quaternion.identity, _spawnHolderEnemy.transform);
       var newEnemyControlle = newEnemy.GetComponent<Controllfc>();
       newEnemyControlle.Botfc = true;
       newEnemyControlle.OffOutline();
       // _layerfc[i+1].SetPlayercOntroller(characterController);
      }
    }
   
    private void Update()
    {
      if (gamestartedfc)
      {
        if (curMinionCountfc < minionCountfc && totalMinionsNewfc < 145){
          CreateMinionfc(false, null, 0);
        }

        if (leadernumfc <= 1 && !gamefinishedfc){
          GameFinishfc(true);
        }
      }

      if (gamefinishedfc){
        if (victoryfc){
          float ts = Time.timeScale;
          ts -= Time.unscaledDeltaTime*0.5f;
          if (ts < 0) ts = 0;
          Time.timeScale = ts;
        }
      }
    }

    public void StartGamefc()
    {
      int index = 0;
      foreach(GameObject character in GameObject.FindGameObjectsWithTag("Player"))
      {
        var controller = character.GetComponent<Controllfc>();
        controller.InitCharacterfc();
        _layerfc[index].SetPlayercOntroller(controller);
        index++;
      }
      
      while ( curMinionCountfc < minionCountfc)
      {
        CreateMinionfc(true, null, 0);
      }
      minionCountfc += 5;
      PlayersInformerfc.SetActive(true);
      MenuUIfc.SetActive(false);
      GameMenuUIfc.SetActive(true);
      gamestartedfc = true;
      //GetComponent<AudioSource>().Play();
    }
    
    public void WriteDeathLeaderboardfc(string nick, bool bot)
    {
      leadernumfc -= 1;
      leaderboardfc[leadernumfc] = "#"+ (leadernumfc+1) + "  " + nick;
      if (!bot) playernumfc = leadernumfc;
    }

    public void CreateMinionfc(bool atStart, GameObject minion, int types){ // 0 = new, 1 = old, 2 - minion = bot gameobject,
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

        if (PrefabsPlayer){
          newPos = PrefabsPlayer.transform.position + new Vector3(x,0,y)*coef;
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
          if ((curMinionCountfc < minionCountfc) || types == 2){
            minion =  Instantiate(minionPrefabfc, newPos, minionPrefabfc.transform.rotation);
            minion.GetComponent<Minionfc>().GameMangerfc = this;
            totalMinionsNewfc += 1;
          }
        }

        minion.transform.position = newPos;
        minion.transform.eulerAngles = Vector3.up*Random.Range(0, 360);
        curMinionCountfc += 1;
        //if (types != 2) curMinionCount += 1;
      }else{
        if (minion && types != 2){
          minion.transform.position = Vector3.up*3000;
          if (types == 1){
            Destroy(minion);
            curMinionCountfc -= 1;
          }
        }
      }


    }

    public void DoVibrofc()
    {
      //HapticPatterns.PlayEmphasis(0.01f, 0.0f);
      Taptic.Light();
    }

    public void GetGoldfc(int gld)
    {
      PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold")+gld);
    }

    public void GameFinishfc(bool win){
      if (!gamefinishedfc){
        if (win){
          WriteDeathLeaderboardfc(PlayerPrefs.GetString("Nickname"), false);
          victoryfc = true;
        }else{
          FinishUIfc.GetComponent<FinishUIfc>().endTextfc.text = "TRY AGAIN";
        }

        PlayersInformerfc.SetActive(false);

        gamefinishedfc = true;

        if (playernumfc > 2) GetGoldfc(15);
        if (playernumfc == 2) GetGoldfc(25);
        if (playernumfc == 1) GetGoldfc(30);
        if (playernumfc == 0) GetGoldfc(50);


        // if (metrica){
        //     string wi = "lose";
        //     if(win) wi = "win";
        //     int progress = (int)( ( (6.0f-playernum)/6.0f )*100 );
        //    metrica.level_finish(1, "01_level", 999, wi, progress, 0);
        // }
        FinishUIfc.SetActive(true);
      }
    }
    
    public string GenerateNicknamefc()
    {
      string nicks = "";
      string names = "Lorder,Golum,Svego,Scratch,Minior,Scooter,TrailBoom,Bomber,Crot,Polos,CretoGard,Creamor,Scremer,Dread!!,Tormant,Grotar,trender,Porter,Potter,Mikel,Dragos,Crang,Creos,Lopas,Kayle,Toodas,Gababa,PilionLorak,Tommy,Dreamer,Josef,Joque,Kinderos,Solan,Sonar,Talos,Tanos,Ketozzzz,Dodo,Foley,Fooooty,Mikilos,Positron,Torman,Vivi,Jios,Pisko,Gurad,Jonny,Xeros,Zooomer,Zombie,velos,pooner,Spil,queqweqwe,asdas,qwee," +
                     "Player35165,Player5466,Player9873,Player1264,Player7897,Player4564,Player9863,Player1216,Player1233,Player4632,Player7536,Player2678,Player1414,Player3326,Player1366,Player8422,Player9613,Player1499,Player4253,Player17655,Player5549,Player5741,Player2896,Player2137,Player9651," +
                     "BloodyKnight,xAngeLx,vlom,Maels,oskar61,wanderer_from,amaze,Z1KkY,Crysler,heletch,shipilov,Chacha,usist,zingo,excurs,capitan_beans,Cashish,LUNTIK,gour,The knyazzz,American_SnIper,NIGHTMARE,007up,Dr.Dizel,RaNDoM,sportik,Su1c1de,Roger,glx506,volandband,pas,Necron,edik_lukoyanov,Synchromesh,SolomA,Dron128,DeatHSoul,DangErXeTER,Psy,Forcas,Morgot,Aspect,Kraken,Bender,Lynch,Big Papa,Mad Dog,Bowser,O’Doyle,Bruise,Psycho,Cannon,Ranger,Clink,Ratchet,Cobra,Reaper,Colt," +
                     "Rigs,Crank,Ripley,Creep,Roadkill,Daemon,Ronin,Decay,Rubble,Diablo,Sasquatch,Doom,Scar,Dracula,Shiver,Dragon,Skinner,Fender,Skull Crusher,Fester,Slasher,Fisheye,Steelshot,Flack,Surge,Gargoyle,Sythe,Grave,Trip,Gunner,Trooper,Hash,Tweek,Hashtag,Vein,Indominus,Void,Ironclad,Wardon,Killer,Wraith,Knuckles,Zero,Steel,Kevlar,Lightning,Tito,Bullet-Proof,Fire-Bred,Titanium,Hurricane,Ironsides,Iron-Cut,Tempest,Iron Heart,Steel Forge,Pursuit,Steel Foil,Upsurge,Uprising," +
                     "Overthrow,Breaker,Sabotage,Dissent,Subversion,Rebellion,Insurgent,Loch,Golem,Wendigo,Rex,Hydra,Behemoth,Balrog,Manticore,Gorgon,Basilisk,Minotaur,Leviathan,Cerberus,Mothman,Sylla,Charybdis,Orthros,Baal,Cyclops,Satyr,Azrael,Mariy_Kis,KATUSHA,KinDer,Eva,BoSoranY,AlfabetkA,ANGEL";
      string[] selectedName = names.Split(char.Parse(","));
      nicks = selectedName[Random.Range(0, selectedName.Length)];
      return nicks;
    }
  }
}
