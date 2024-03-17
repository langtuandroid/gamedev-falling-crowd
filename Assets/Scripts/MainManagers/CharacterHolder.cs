using System.Collections.Generic;
using UnityEngine;

namespace MainManagers
{
    public class CharacterHolder : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _characterModels;
        [SerializeField] 
        private List<CharacterData> _characterDatas;

        public List<GameObject> CharacterModels => _characterModels;

        public List<CharacterData> CharacterDatas => _characterDatas;
    }
}
