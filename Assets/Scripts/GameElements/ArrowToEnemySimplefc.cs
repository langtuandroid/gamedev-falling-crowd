using UnityEngine;

namespace GameElements
{
  public class ArrowToEnemySimplefc : MonoBehaviour
  {
    public GameObject Enemyfc;
    private float timerfc;
    public bool startedfc = true;
    public SpriteRenderer imgfc;
   
    public void SetStart(GameObject g, Color col)
    {
      Enemyfc = g;
      startedfc = true;
      imgfc.color = col;
      //timer = Random.Range(0.01f, 0.43f);
    }
    
    private void Update()
    {
      /*timer -= Time.deltaTime;
      if (timer < 0){
        timer = 0.2f;*/
      if (startedfc){
        if (Enemyfc){
          if (Vector3.Distance(transform.position, Enemyfc.transform.position) > 5){
            transform.LookAt(Enemyfc.transform);
          }else{
            transform.eulerAngles = new Vector3(90,0,0);
          }
        }else{
          Destroy(gameObject);
        }
      }
    }
  }
}
