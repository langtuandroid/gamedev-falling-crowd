using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowToEnemy : MonoBehaviour
{
    public GameObject Enemy;
    private float timer;
    public bool started = true;
    public Image img;
    // Start is called before the first frame update
    public void SetStart(GameObject g, Color col)
    {
      Enemy = g;
      //started = true;
      img.color = col;
      //timer = Random.Range(0.01f, 0.43f);
    }

    // Update is called once per frame
    void Update()
    {
      /*timer -= Time.deltaTime;
      if (timer < 0){
        timer = 0.2f;*/
        if (started){
          if (Enemy){
            IndicateEnemy();
          }else{
            Destroy(gameObject);
          }
        }

    }

    void IndicateEnemy(){
      var screenPos = Camera.main.WorldToViewportPoint(Enemy.transform.position);

      var onScreenPos = new Vector2(screenPos.x-0.5f, screenPos.y-0.5f)*2;
      var max = Mathf.Max(Mathf.Abs(onScreenPos.x), Mathf.Abs(onScreenPos.y));
      onScreenPos = (onScreenPos/(max*2))+new Vector2(0.5f, 0.5f);
      var screenPoint = Camera.main.ViewportToScreenPoint(onScreenPos);

      RotateToTargetPoint(transform, screenPos, onScreenPos);
      transform.position = screenPoint;
      //transform.position = new Vector2(screenPoint.x-0.5f, screenPoint.y-0.5f)*2;;
    }





    private void RotateToTargetPoint(Transform arrow,Vector3 screenPos, Vector3 target)
    {
                var dir = target - screenPos;
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                arrow.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
