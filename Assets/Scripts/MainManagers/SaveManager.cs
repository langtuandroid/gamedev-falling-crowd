using UnityEngine;

public static class SaveLoadManager 
{
        static string COINSKEY = "Gold";
        static string HEROMODELKEY = "HEROMODEL";
        static string CHOSECHARACTER = "CHOSECHARACTER";
        static string CHARACTERSELECT = "CHARACTERSELECT";

        public static void SaveSettings(string key, bool state)
        {
            PlayerPrefs.SetInt(key, state ? 1 : 0);
            PlayerPrefs.Save();
        }
         
        public  static bool LoadSettings(string key)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                SaveSettings(key, false);
                PlayerPrefs.Save();
            }
            return PlayerPrefs.GetInt(key) == 1;
        }


        public static int LoadCoins()
        {
            return PlayerPrefs.GetInt(COINSKEY);
        }

        public static void SaveCoins(int coinsAmount)
        {
            PlayerPrefs.SetInt(COINSKEY, coinsAmount);
        }

        public static int GetSelectHeroModel()
        {
            if (!PlayerPrefs.HasKey(HEROMODELKEY))
            {
                SaveSelectHeroModel(0);
            }
            
            return PlayerPrefs.GetInt(HEROMODELKEY);
        }

        public static void SaveSelectHeroModel(int indexCharacter)
        { PlayerPrefs.SetInt(HEROMODELKEY, indexCharacter); }


        public static int LoadChoseCharacter()
        {
            if (!PlayerPrefs.HasKey(CHOSECHARACTER))
            {
                SaveChoseCharacter(0);
            }
          
            return PlayerPrefs.GetInt(CHOSECHARACTER);
        }

        public static void SaveChoseCharacter(int itemIndex)
        {
            PlayerPrefs.SetInt(CHOSECHARACTER,itemIndex);
        }
        

        public static int LoadCharacter()
        {
            if (!PlayerPrefs.HasKey(CHARACTERSELECT))
            {
                SaveCharacter(0);
            }
            return PlayerPrefs.GetInt(CHARACTERSELECT);
        }

        public static void SaveCharacter(int index)
        {
            PlayerPrefs.SetInt(CHARACTERSELECT, index);
        }
    
}
