using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueControl : MonoBehaviour
{
    [HideInInspector] public Queue<GameObject> atmUsers;
    private GameObject atmUser;

    void Start()
    {
        atmUsers = new Queue<GameObject>();
    }

    public void AddToQueue(GameObject atmUserToAdd)
    {
        atmUsers.Enqueue(atmUserToAdd);
    }

    public void RemoveFromQueue()
    {
        atmUsers.Dequeue();
    }

    public void EmptyQueue()
    {
        atmUsers.Clear();
    }

}
