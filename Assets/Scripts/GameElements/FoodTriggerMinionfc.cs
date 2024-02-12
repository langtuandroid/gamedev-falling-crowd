using UnityEngine;

namespace GameElements
{
  public class FoodTriggerMinionfc : MonoBehaviour
  {
    private void OnTriggerEnter(Collider col)
    {
      if (col.CompareTag("Player"))
      {
        col.GetComponent<Controll>().MinionGet(gameObject);
      }
      if (col.CompareTag("Minion"))
      {
        col.GetComponent<Minion>().player.GetComponent<Controll>().MinionGet(gameObject);
      }
    }
  }
}
