using UnityEngine;

namespace GameElements
{
    public class OnHexfc : MonoBehaviour
    {
        private enum HexStep
        {
            Descend,
            Ascend,
            LowerDescend,
            Rise,
            LowerFinal,
            Idle,
            Finalize
        }
    
        private HexStep step = HexStep.Descend;
        private float timerfc;
        private Material matfc;
        private Color startColorfc;
   
        private void Start()
        {
            matfc = GetComponent<MeshRenderer>().material;
            startColorfc = matfc.color;
        }

        private void Update()
        {
            switch (step)
            {
                case HexStep.Descend:
                    transform.Translate(Vector3.down * Time.deltaTime / 5);
                    if (transform.position.y < -1.1f) step = HexStep.Ascend;
                    break;
                case HexStep.Ascend:
                    transform.Translate(Vector3.up * Time.deltaTime / 5);
                    if (transform.position.y > -0.96f) step = HexStep.LowerDescend;
                    break;
                case HexStep.LowerDescend:
                    transform.Translate(Vector3.down * Time.deltaTime / 3);
                    if (transform.position.y < -1.1f) step = HexStep.Rise;
                    break;
                case HexStep.Rise:
                    transform.Translate(Vector3.up * Time.deltaTime / 4);
                    if (transform.position.y > -0.96f)
                    {
                        step = HexStep.LowerFinal;
                        GetComponent<SphereCollider>().enabled = false;
                    }
                    break;
                case HexStep.LowerFinal:
                    transform.Translate(Vector3.down * Time.deltaTime * 2);
                    if (transform.position.y < -3)
                    {
                        step = HexStep.Idle;
                        matfc.color = startColorfc;
                    }
                    break;
                case HexStep.Idle:
                    timerfc += Time.deltaTime;
                    if (timerfc > 10)
                    {
                        transform.Translate(Vector3.up * Time.deltaTime * 2);
                        if (transform.position.y > -1) step = HexStep.Finalize;
                    }
                    break;
                case HexStep.Finalize:
                    transform.position = new Vector3(transform.position.x, -1, transform.position.z);
                    gameObject.tag = "Hex";
                    GetComponent<SphereCollider>().enabled = true;
                    Destroy(this);
                    break;
            }

            if (step < HexStep.Idle)
            {
                matfc.color = Color.Lerp(matfc.color, new Color(0.9f, 0.26f, 0.26f), Time.deltaTime * 0.3f);
            }
        }
    
    }
}
