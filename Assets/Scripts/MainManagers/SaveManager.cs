using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveLoadManager 
{
        static string COINSKEY = "Gold";
        static string HEROMODELKEY = "HEROMODEL";
        static string ITEMUNLOCKEDKEY = "ITEMUNLOCKED";
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

        
        public static int GetItemUnlockedState(int itemIndex)
        { return PlayerPrefs.GetInt(ITEMUNLOCKEDKEY + itemIndex); }

        public static void SetItemUnlockedState(int itemIndex, int state)
        { PlayerPrefs.SetInt(ITEMUNLOCKEDKEY + itemIndex, state); }
        

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
