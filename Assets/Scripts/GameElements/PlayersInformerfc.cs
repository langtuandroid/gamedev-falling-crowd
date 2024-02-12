using UnityEngine;

namespace GameElements
{
    public class PlayersInformerfc : MonoBehaviour
    {
        public GameObject[] placesfc;
        public Transform layersfc;
        private float timerfc;
        private int numberfc;

        private void Update()
        {
            timerfc += Time.deltaTime;

            if (timerfc > 0.5f){
                timerfc = 0;
                numberfc = 0;
                foreach(Transform t in layersfc){
                    t.position = placesfc[numberfc].transform.position;
                    numberfc += 1;
                }
            }

        }
    }
}
