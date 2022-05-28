using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScaleNavigator : MonoBehaviour
{
    public GameObject bigScaleObject;

    private Vector3 bigScalePosition;
    private Animator bigAnimator;
    private Animator smallAnimator;
    private NavMeshAgent agent;

    void Start()
    {
        agent = bigScaleObject.GetComponent<NavMeshAgent>(); 
        bigAnimator = bigScaleObject.GetComponent<Animator>();
        smallAnimator = this.GetComponent<Animator>();
    }

    void Update()
    {
        if (bigScaleObject.activeSelf)
        {
            smallAnimator.runtimeAnimatorController = bigAnimator.runtimeAnimatorController;
            bigScalePosition = bigScaleObject.transform.localPosition;
            this.transform.localPosition = bigScalePosition / 100f;
            this.transform.eulerAngles = bigScaleObject.transform.eulerAngles;
        }
        else
		{
            Destroy(this.gameObject, 1);
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("SlowDown"))
        {
            agent.speed = 1;
        }

        if (other.CompareTag("NormalDoor"))
        {
            StartCoroutine(waitSomeSeconds());
        }

        if (other.CompareTag("SpeedUp"))
        {
            agent.speed = 4;
        }

        if (other.CompareTag("AutoDoor"))
        {
            StartCoroutine(waitSomeSeconds());
        }
    }

    IEnumerator waitSomeSeconds()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(1);
        agent.isStopped = false;

    }

}
