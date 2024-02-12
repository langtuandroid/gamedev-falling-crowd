using MainManagers;
using TMPro;
using UnityEngine;

namespace UIElements
{
  public class FinishUIfc : MonoBehaviour
  {
    public TextMeshProUGUI endTextfc;
    public TextMeshProUGUI[] textLeaderfc;
    
    [SerializeField]
    private TextMeshProUGUI _coins;
    private int nummaxfc;
    private float timerfc;
    private GameManagerfc GameManagerfc;
    private void Start()
    {
      GameManagerfc = GameObject.Find("GameManager").GetComponent<GameManagerfc>();
      nummaxfc = GameManagerfc.leaderboardfc.Length;
    }
    
    private void Update()
    {
      timerfc -= Time.deltaTime;
      if (timerfc < 0){
        _coins.text = PlayerPrefs.GetInt("Gold") + "";
        timerfc = 0.5f;

        int i = 0;
        while (i < nummaxfc)
        {
          if (GameManagerfc.leaderboardfc[i] != "")
          {
            textLeaderfc[i].text = GameManagerfc.leaderboardfc[i];
            if (GameManagerfc.playernumfc == i) textLeaderfc[i].color = new Color(1,1,0);
          }
          i++;
        }
      }
    }

    public void ButtonRestartfc()
    {
      Application.LoadLevel(Application.loadedLevel);
    }
  }
}

