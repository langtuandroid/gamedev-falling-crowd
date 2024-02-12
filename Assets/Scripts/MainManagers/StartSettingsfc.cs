using UnityEngine;

namespace MainManagers
{
    public class StartSettingsfc : MonoBehaviour
    {
        private void Start()
        {
            if (PlayerPrefs.GetInt("firstStart") != 1)
            {
                PlayerPrefs.SetInt("firstStart", 1);
                PlayerPrefs.SetString("Nickname", "Player" + Random.Range(11212, 99999));
            }
        }
    }
}
