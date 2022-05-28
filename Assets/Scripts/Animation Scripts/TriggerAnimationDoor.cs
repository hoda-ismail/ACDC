using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TriggerAnimationDoor : MonoBehaviour
{
    // help from https://www.youtube.com/watch?v=kqBGg6Rme10
    [SerializeField] private Animator myDoor;
    [SerializeField] private string doorCondition;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Character"))
        {
            myDoor.Play(doorCondition, 0, 0);
        }
        
    }
}
