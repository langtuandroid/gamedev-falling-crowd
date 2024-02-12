using UnityEngine;

namespace Other
{
    public class OnTextLayerfc : MonoBehaviour
    {
        public int Orderfc;
        private void Start()
        {
            if (Orderfc == 0) Orderfc = 600;
            gameObject.GetComponent<MeshRenderer> ().sortingOrder = Orderfc;
        }
    }
}
