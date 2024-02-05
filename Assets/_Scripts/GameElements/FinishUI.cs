using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishUI : MonoBehaviour
{
    private GameManager GM;
    private float timer;

    public Text[] textLeader;
    private int nummax;
    public Text endText;
    public Text t_coins;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        nummax = GM.leaderboard.Length;
    }

    // Update is called once per frame
    void Update()
    {
      timer -= Time.deltaTime;
      if (timer < 0){
        t_coins.text = PlayerPrefs.GetInt("Gold") + "";
        timer = 0.5f;

        int i = 0;
        while (i < nummax){
          if (GM.leaderboard[i] != ""){
             textLeader[i].text = GM.leaderboard[i];
             if (GM.playernum == i) textLeader[i].color = new Color(1,1,0);
           }
          i++;
        }
      }
    }

    public void ButtonRestart(){
      Application.LoadLevel(Application.loadedLevel);
    }
}
