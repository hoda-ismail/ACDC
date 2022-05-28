using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float bounds = 1f;

    // Update is called once per frame
    void Update()
    {
        // if out of given bounds then destroy
        if (transform.position.magnitude > bounds) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        Destroy(gameObject);
    }

    // this is changed to destroy just to reduce amounts of models in the simulation
    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);   
    }
}
