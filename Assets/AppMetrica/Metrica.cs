using System.Collections.Generic;
using UnityEngine;

public class Metrica : MonoBehaviour
{

    private float gameTimer;
    private float gameTimerStage;
    private int lvl_count;

    private float timerLoad;

    void Start()
    {
        lvl_count = PlayerPrefs.GetInt("lvl_count");
        /*
        if (lvl_count == 0){
          lvl_count = 1;
          PlayerPrefs.SetInt("lvl_count", 1);
        }*/

        DontDestroyOnLoad(gameObject);
    }

    void Update(){
        gameTimer += Time.deltaTime;
        gameTimerStage += Time.deltaTime;

        if (timerLoad >= 0){
          timerLoad += Time.deltaTime;
          if (timerLoad > 1.6f){
            timerLoad = -9999;
            Application.LoadLevel("Game");
          }
        }
    }

    public void MetricaSender(string eventName, Dictionary<string, object> dict, bool sendNow){
      var parameters = dict;
      //=================================================================================
        /*    bool pay = false;
              if (PlayerPrefs.GetInt("payer") == 1) pay = true;
            bool is_boosted = false;
              if (PlayerPrefs.GetInt("is_boosted") == 1) is_boosted = true;

             parameters.Add("stage_global_count", PlayerPrefs.GetInt("stage_global_count") );
             parameters.Add("level_count", PlayerPrefs.GetInt("LevelCount") );
             parameters.Add("upgrade_count", PlayerPrefs.GetInt("upgrade_count") );
             parameters.Add("hard_balance", PlayerPrefs.GetInt("Gold") );
             parameters.Add("payer", pay );
             parameters.Add("is_boosted", is_boosted );
*/
      //=================================================================================

      // AppMetrica.Instance.ReportEvent (eventName, parameters);
      // if (sendNow) AppMetrica.Instance.SendEventsBuffer();
    }

    // Update is called once per frame
    public  void level_start(int level_number,
                            string level_name,
                            int level_count)
    {
      gameTimer = 0;
            lvl_count += 1;
            PlayerPrefs.SetInt("lvl_count", lvl_count);

      Dictionary<string, object> parameters = new Dictionary<string, object>();

      parameters.Add("level_number", level_number );
      parameters.Add("level_name", level_name );
      parameters.Add("level_count", lvl_count );
      parameters.Add("level_diff", "easy" );
      //parameters.Add("level_day", level_loop );
      //parameters.Add("level_random", 0 );
      parameters.Add("level_type", "normal" );
      parameters.Add("game_mode", "classic" );



      MetricaSender("level_start", parameters, true);
    }

    public  void level_finish(int level_number,
                             string level_name,
                             int level_count,
                             string winlose,
                             int progress,
                             int continues)
    {

      //if (winlose == "win") progress = 100;

      Dictionary<string, object> parameters2 = new Dictionary<string, object>();
      parameters2.Add("level_number", level_number );
      parameters2.Add("level_name", level_name );
      parameters2.Add("level_count", lvl_count );
      parameters2.Add("level_diff", "easy" );
      parameters2.Add("level_random", 0 );
      parameters2.Add("level_type", "normal" );
      parameters2.Add("game_mode", "classic" );

      parameters2.Add("result", winlose );
      parameters2.Add("progress",  progress);
      parameters2.Add("time", (int)gameTimer );
      parameters2.Add("continue", continues );

      MetricaSender("level_finish", parameters2, true);
    }

    // Update is called once per frame
    public  void stage_start(int level_loop)
    {
      gameTimerStage = 0;
      Dictionary<string, object> parameters = new Dictionary<string, object>();
      parameters.Add("level_loop", level_loop );
      MetricaSender("stage_start", parameters, true);
    }


    public  void stage_finish(int stage_number,
                             string winlose,
                             int continuesDay,
                             int score,
                             GameManager GM)
    {

      Dictionary<string, object> parameters2 = new Dictionary<string, object>();

      parameters2.Add("stage_number", stage_number );
      parameters2.Add("result", winlose );
      parameters2.Add("time", gameTimerStage );
      parameters2.Add("continue", continuesDay );
      parameters2.Add("task_complete", continuesDay );
      parameters2.Add("score", score );

      MetricaSender("stage_finish", parameters2, true);
    }


    public  void skin_unlock(string skin_type,
                             string skin_name,
                             string skin_rarity,
                             string unlock_type )
    {

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("skin_type", skin_type );  //// weapon, armor, hero etc.
            parameters.Add("skin_name", skin_name );
            parameters.Add("skin_rarity", skin_rarity );  //// Редкость скина - common, epic etc.
            parameters.Add("unlock_type", unlock_type );  //// random_ads, specific_ads, random_currency, specific_achievement, specific_currency

            MetricaSender("skin_unlock", parameters, false);
    }


        public  void rate_us(string show_reason,
                                 int rate_result)
        {

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("show_reason", show_reason );  //// new_version, new_player etc....
                parameters.Add("rate_result", rate_result );   //// 0 - closed, 1-5 stars

                MetricaSender("rate_us", parameters, false);
        }

        public  void task_complete(int stage_number,
                                 string task_type,
                                 string task_name,
                                 int task_reward )
        {

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("stage_number", stage_number );
                parameters.Add("task_type", task_type );
                parameters.Add("task_name", task_name );
                parameters.Add("task_reward", task_reward );

                MetricaSender("task_complete", parameters, false);
        }

        public  void hard_currency(string item_id,
                                 int value,
                                 string category )
        {

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("item_id", item_id );
                parameters.Add("value", value );
                parameters.Add("category", category );

                MetricaSender("hard_currency", parameters, false);
        }

        public  void menu_action(string action_type,
                                 string change_id )
        {

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("action_type", action_type );
                if (change_id != "null") parameters.Add("change_id", change_id );

                MetricaSender("menu_action", parameters, false);
        }

        public  void pop_up(string pop_up_id,
                                 string show_reason,
                                 string result )
        {

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("pop_up_id", pop_up_id );
                parameters.Add("change_id", show_reason );
                parameters.Add("result", result );

                MetricaSender("pop_up", parameters, false);
        }









        public  void video_ads_watch(string ad_type,
                                 string placement,
                                 string result,
                                 bool connection )
        {

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("ad_type", ad_type );
                parameters.Add("placement", placement );
                parameters.Add("result", result );
                parameters.Add("connection", connection );

                MetricaSender("video_ads_watch", parameters, false);
        }

        public  void video_ads_available(string ad_type,
                                 string placement,
                                 string result,
                                 bool connection )
        {

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("ad_type", ad_type );
                parameters.Add("placement", placement );
                parameters.Add("result", result );
                parameters.Add("connection", connection );

                MetricaSender("video_ads_available", parameters, false);
        }

        public  void video_ads_started(string ad_type,
                                 string placement,
                                 string result,
                                 bool connection )
        {

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("ad_type", ad_type );
                parameters.Add("placement", placement );
                parameters.Add("result", result );
                parameters.Add("connection", connection );

                MetricaSender("video_ads_started", parameters, false);
        }

        public  void technical(string step_name)
        {
                string sname = "null";
                if (step_name == "applovin") sname = "01_initialize_applovin";
                if (step_name == "gdpr") sname = "02_gdpr_check";
                if (step_name == "loadscene") sname = "03_load_scene_menu";
                if (step_name == "appsflyer") sname = "04_initialize_appsflyer";
                if (step_name == "finish") sname = "05_complete";

                bool first_start = true;
                if (PlayerPrefs.GetInt("TechStart") == 1) first_start = false;
                //Debug.Log(sname);

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("step_name", sname );
                parameters.Add("first_start", first_start );

                MetricaSender("technical", parameters, true);
        }
          //    if (metrica) metrica.video_ads_watch("rewarded", paramReward, "watched", GetComponent<CheckInternet>().InternetStatus);


}
