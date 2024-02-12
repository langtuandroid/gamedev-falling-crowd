using UnityEngine;

namespace Other
{
    public class ScaleZeroPosfc : MonoBehaviour
    {
        private float timerfc;
        private void Start()
        {
            transform.localScale = Vector3.zero;
            Destroy(this, 4);
        }
        
        private void Update()
        {
            timerfc += Time.deltaTime;
            //  if (timer > 0.01f){
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one , Time.deltaTime*15);
            //  }
        }
    }
}
