using MainManagers;
using UnityEngine;

namespace GameElements
{
  public class FinderColliderfc : MonoBehaviour
  {
    public GameObject objectFindfc;
    private Controllfc controllfc;
    private float timerDelayfc = -1;
    private float delayfc = 3;
    
    public void SetStart()
    {
      controllfc = transform.parent.GetComponent<Controllfc>();
      transform.parent = null;
    }

    private void Update()
    {
      timerDelayfc -= Time.deltaTime;
    }

    private void OnTriggerStay(Collider col)
    {
      if (timerDelayfc < 0)
      {
        if (col.CompareTag("Food"))
        {
          objectFindfc = col.gameObject;
          timerDelayfc = delayfc;
          controllfc.targetfc = objectFindfc;
        }
      }
    }
  }
}
