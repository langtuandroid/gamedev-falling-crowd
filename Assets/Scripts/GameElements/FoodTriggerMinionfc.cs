using MainManagers;
using UnityEngine;

namespace GameElements
{
  public class FoodTriggerMinionfc : MonoBehaviour
  {
    private void OnTriggerEnter(Collider col)
    {
      if (col.CompareTag("Player"))
      {
        col.GetComponent<Controllfc>().MinionGetfc(gameObject);
      }
      if (col.CompareTag("Minion"))
      {
        col.GetComponent<Minionfc>().playerfc.GetComponent<Controllfc>().MinionGetfc(gameObject);
      }
    }
  }
}
