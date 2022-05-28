using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = .1f;
    private bool turnaround = false;

    // Update is called once per frame
    void Update() {

        MoveForward(); 
        
        if(turnaround) {
            TurnAround();
        }
    }

    void MoveForward() {
        float step = Time.deltaTime * speed;
        transform.Translate(transform.forward * step, Space.World);
    }

    void TurnAround() {
        // rotate around
        transform.Rotate(0, Time.deltaTime * 30, 0, Space.Self);
    }



    private void OnTriggerEnter(Collider other) {
        turnaround = true;
    }
}
