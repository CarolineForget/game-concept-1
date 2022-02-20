using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private int number;
    private int nb_fallen = 0;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("WithForLoop", 6f);
    }

    void WithForLoop()
     {
         number = Mathf.CeilToInt(Random.Range(0, 8f));
         //Debug.Log(number);

         int children = transform.childCount;
         for (int i = 0; i < children; i++) {
             Transform child = transform.GetChild(number);
             
             
             FallenFloors(child);
         }

         Invoke("WithForLoop", 6f);
             
     }

     private void FallenFloors(Transform go) {
         go.gameObject.SetActive(false);
         //Debug.Log(nb_fallen);
     }

     private void ActiveFloor(Transform go) {
         go.gameObject.SetActive(true);
     }
}
