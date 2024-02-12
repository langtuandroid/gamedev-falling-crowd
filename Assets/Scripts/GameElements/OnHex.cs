using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHex : MonoBehaviour
{
    private int step;
    private float timer;
    private Material mat;
    private Color startColor;
    // Start is called before the first frame update
    void Start()
    {
    //  GetComponent<SphereCollider>().enabled = false;
      mat = GetComponent<MeshRenderer>().material;
      startColor = mat.color;
      //gameObject.tag = "HexOn";
    }

    // Update is called once per frame
    void Update()
    {
      if (step < 5){
        mat.color = Color.Lerp(mat.color, new Color(0.9f, 0.26f, 0.26f), Time.deltaTime*0.3f);
      }

      if (step == 0){
        transform.position -= Vector3.up*Time.deltaTime/5;
        if (transform.position.y < -1.1f) step = 1;
      }else if (step == 1){
        transform.position += Vector3.up*Time.deltaTime/5;
        if (transform.position.y > -0.96f) step = 2;
      }else if (step == 2){
        transform.position -= Vector3.up*Time.deltaTime/3;
        if (transform.position.y < -1.1f) step = 3;
      }else if (step == 3){
        transform.position += Vector3.up*Time.deltaTime/4;
        if (transform.position.y > -0.96f){
           step = 4;
           GetComponent<SphereCollider>().enabled = false;
         }
      }else if (step == 4){
        transform.position -= Vector3.up*Time.deltaTime*2;
        if (transform.position.y < -3){
           step = 5;
           mat.color = startColor;
         }
      }else if (step == 5){
        timer += Time.deltaTime;
        if (timer > 10){
          transform.position += Vector3.up*Time.deltaTime*2;
          if (transform.position.y > -1) step = 6;
        }
      }else if (step == 6){
        transform.position = new Vector3(transform.position.x,-1, transform.position.z);
        gameObject.tag = "Hex";
        GetComponent<SphereCollider>().enabled = true;
        Destroy(this);
      }
    }
}
