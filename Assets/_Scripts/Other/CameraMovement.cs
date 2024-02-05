using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public Vector3 vectorCorrection;

    //public float minZ;

    public GameObject hero;

    private float view;
    private bool doview;
    private float correct;
    void Start()
    {
        //hero = GameObject.Find("Hero");
        view = 60;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (hero) transform.position = Vector3.Lerp(transform.position, hero.transform.position + vectorCorrection, Time.deltaTime*speed);

         if (doview)
          {
              Camera.main.orthographicSize  += Time.deltaTime*correct*7;
              if (correct == 1)
              {
                  if (Camera.main.orthographicSize  > view)
                  {
                      Camera.main.orthographicSize  = view;
                      doview = false;
                  }
              }
              else
              {
                  if (Camera.main.orthographicSize  < view)
                  {
                      Camera.main.orthographicSize  = view;
                      doview = false;
                  }
              }
          }

    }

    public void SetFieldView(Vector2 vec)
    {
        view = vec.x;
        correct = vec.y;
        doview = true;
    }
}
