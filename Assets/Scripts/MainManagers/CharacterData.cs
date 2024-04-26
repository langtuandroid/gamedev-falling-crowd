using UnityEngine;


[CreateAssetMenu(fileName = "CharacterData", menuName = "CharacterDataScriptableObjects")]
public class CharacterData : ScriptableObject
{
   [SerializeField] 
   private GameObject _prefab;
   [SerializeField] 
   private Color _color;
   [SerializeField] 
   private float _speed;
   [SerializeField] 
   private int _price;
   [SerializeField] 
   private int _adsWatchCounter;
   [SerializeField] 
   private bool _isUnlockStatus;
   [SerializeField] 
   private TypeUnlock _typeUnlock;

   public GameObject Prefab => _prefab;
   public Color Color => _color;
   public float Speed => _speed;
   public int Price => _price;

   
   public TypeUnlock TypeUnlock => _typeUnlock;

   public bool IsUnlockStatus
   {
      get => _isUnlockStatus;
      set => _isUnlockStatus = value;
   }

   public int AdsWatchCounter
   {
       get => _adsWatchCounter;
       set => _adsWatchCounter = value;
   }
}

public enum TypeUnlock
{
     ForCoins,
     ForDiamonds,
     ForAds
}
