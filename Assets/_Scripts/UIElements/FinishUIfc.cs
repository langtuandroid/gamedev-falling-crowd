using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scripts.UIElements
{
  public class FinishUIfc : MonoBehaviour
  {
    private GameManager GM;
    private float timer;

    public Text[] textLeader;
    private int nummax;
    public Text endText;
    [SerializeField]
    public TextMeshProUGUI _coins;
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
        _coins.text = PlayerPrefs.GetInt("Gold") + "";
        timer = 0.5f;

        int i = 0;
        while (i < nummax){
          if (GM.leaderboard[i] != "")
          {
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
}
