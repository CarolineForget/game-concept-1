using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{

    [SerializeField] private GameObject parent;

    [SerializeField] private Transform boxPfb;
    private Transform box;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnBoxes", 2f);
    }

    private void SpawnBoxes() {
        //box = Instantiate(boxPfb, new Vector3(Random.Range(-20f, 20f), 20f, Random.Range(-20f, 15f)), Quaternion.identity, parent.transform);
        box = Instantiate(boxPfb, new Vector3(Random.Range(-7.5f, 20f), 20f, Random.Range(-20f, 2f)), Quaternion.identity, parent.transform);
        box.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        box.name = "Box Spawned";

        Invoke("SpawnBoxes", 7f);
    }
}
