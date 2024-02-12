using MainManagers;
using UnityEngine;

namespace GameElements
{
    public class CheckVisiblefc : MonoBehaviour
    {
        private Controllfc _controllfc;
      
        public void SetStart(Controllfc controllfc)
        {
            _controllfc = controllfc;
        }
        
        private void OnBecameVisible()
        {
            _controllfc.visiblefc = true;
        }
        
        private void OnBecameInvisible()
        {
            _controllfc.visiblefc = false;
        }
    }
}
