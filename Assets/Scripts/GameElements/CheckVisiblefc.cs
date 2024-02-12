using UnityEngine;

namespace GameElements
{
    public class CheckVisiblefc : MonoBehaviour
    {
        private Controll _controllfc;
      
        public void SetStart(Controll controllfc)
        {
            _controllfc = controllfc;
        }
        
        private void OnBecameVisible()
        {
            _controllfc.visible = true;
        }
        
        private void OnBecameInvisible()
        {
            _controllfc.visible = false;
        }
    }
}
