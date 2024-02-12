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
      nummaxfc = GameManagerfc.leaderboard.Length;
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
          if (GameManagerfc.leaderboard[i] != "")
          {
            textLeaderfc[i].text = GameManagerfc.leaderboard[i];
            if (GameManagerfc.playernum == i) textLeaderfc[i].color = new Color(1,1,0);
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

