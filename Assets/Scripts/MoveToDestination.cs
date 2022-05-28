using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToDestination : MonoBehaviour
{
    // destionation for the agents to move to
    private Transform destination;
    private Destinations GetDestinations;
    


    // agent component on this game object
    private NavMeshAgent agent;

    public RuntimeAnimatorController idle;
    public RuntimeAnimatorController movement;
    private Animator animator;
    private void Awake() {
        // capturing an agent component and setting destination to self upon initialization
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(transform.position);

        // find object with script Destionations and capture location of the destination assigned there 
        // Script Destionations is placed at CarManager
        GetDestinations = FindObjectOfType<Destinations>();
        destination = GetDestinations.moveTowards;
        
        
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        // check if the destination was moved, update agent's destination
        if (DestinationMoved()) {
            agent.SetDestination(destination.position);
            
        }
        
        // Check if we've reached the destination
        // https://answers.unity.com/questions/324589/how-can-i-tell-when-a-navmesh-has-reached-its-dest.html
        // https://stackoverflow.com/questions/60810676/unity-navmesh-event-on-navigation-end
        if (agent.remainingDistance <= 0.02f) 
        {
            animator.runtimeAnimatorController = idle;
        }
        
        
       

    }


    // destination considered changed the destination of the agent is different than the once fetched from the 
    // Destiantions
    private bool DestinationMoved() {
        
        if (agent.destination != destination.position) {
            animator.runtimeAnimatorController = movement;
            return true;
        }
        return false;
        
        
      
    }

}
