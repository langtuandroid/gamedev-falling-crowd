using UnityEngine;
using UnityEngine.UI;

namespace GameElements
{
  public class ArrowToEnemyfc : MonoBehaviour
  {
    public GameObject Enemyfc;
    private float timerfc;
    public bool startedfc = true;
    public Image imgfc;
    
   
    public void SetStart(GameObject g, Color col)
    {
      Enemyfc = g;
      //started = true;
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
          IndicateEnemyfc();
        }else{
          Destroy(gameObject);
        }
      }
    }

    private void IndicateEnemyfc()
    {
      var screenPos = Camera.main.WorldToViewportPoint(Enemyfc.transform.position);

      var onScreenPos = new Vector2(screenPos.x-0.5f, screenPos.y-0.5f)*2;
      var max = Mathf.Max(Mathf.Abs(onScreenPos.x), Mathf.Abs(onScreenPos.y));
      onScreenPos = (onScreenPos/(max*2))+new Vector2(0.5f, 0.5f);
      var screenPoint = Camera.main.ViewportToScreenPoint(onScreenPos);

      RotateToTargetPointfc(transform, screenPos, onScreenPos);
      transform.position = screenPoint;
      //transform.position = new Vector2(screenPoint.x-0.5f, screenPoint.y-0.5f)*2;;
    }
    
    private void RotateToTargetPointfc(Transform arrow,Vector3 screenPos, Vector3 target)
    {
      var dir = target - screenPos;
      var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
      arrow.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
  }
}
