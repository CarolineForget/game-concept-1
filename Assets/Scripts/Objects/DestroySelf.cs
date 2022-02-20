using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.tag == "Ground") {
            Destroy(gameObject, 1.5f);
        }

    }
}
