using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


//Leos destination implementation
[RequireComponent(typeof(NavMeshAgent))]
public class AgentCharacteristics : MonoBehaviour
{
	[HideInInspector] public Transform startPoint;

	public List<Transform> targetsGameObjects = new List<Transform>();
	public List<Transform> exitGameObjects = new List<Transform>();
	public RuntimeAnimatorController idle;
	public RuntimeAnimatorController movement;
	public bool destroyActive;
	public GameObject startButton;
	public GameObject queueControlGameObject;
	public GameObject removeCharactersGameObject;

	private float startSpeed;
	private bool slowOrFaster;
	private float speedCheck;
	private Animator animator;
	private int numberOfTargets;
	private int currentTarget;
	private bool targetExists;
	private bool isExiting;
	private bool isWaiting;
	private bool isAddedToQueue;
	private QueueControl queueControl;
	private RemoveCharacters removeCharacters;
	private float atmUserPosition;

	Vector3 destination;
	Transform targetDestination;
	NavMeshAgent agent;

	void Start()
	{

		// Cache agent component and destination
		isAddedToQueue = false;
		isWaiting = false;
		removeCharacters = removeCharactersGameObject.GetComponent<RemoveCharacters>();
		agent = GetComponent<NavMeshAgent>();
		queueControl = queueControlGameObject.GetComponent<QueueControl>();
		///Queue
		/*if (agent.gameObject.name.Contains("ATM"))
		{
			queueControl.AddToQueue(agent.gameObject);
			//atmUserPosition = queueControl.atmUsers.Count;
		}*/
		//End Queue isAddedToQueue
		removeCharacters.AddCharacter(agent);

		animator = GetComponent<Animator>();
		destination = agent.destination;
		startSpeed = agent.speed;
		numberOfTargets = targetsGameObjects.Count;
		if (numberOfTargets == 0)
		{
			GoToExit();
			isExiting = true;
			targetExists = false;
		}
		else
		{
			isExiting = false;
			targetExists = true;
			currentTarget = 0;
			targetDestination = targetsGameObjects[0];
			agent.SetDestination(targetDestination.localPosition);
		}
	}

	void Update()
	{
		if (startButton.transform.transform.tag == "Pause")
		{
			if (!isWaiting)
			{
				StartAnimation();
			}

			// Update destination if the target moves one unit

			if (Vector3.Distance(destination, targetDestination.position) > 1)
			{
				destination = targetDestination.position;
				agent.destination = destination;
			}

			// If ATM is not tracked
			if (agent.gameObject.name.Contains("ATM") && !isExiting)
			{
				if (!targetDestination.gameObject.activeSelf)
				{
					GoToExit();
					if (isAddedToQueue)
					{
						queueControl.RemoveFromQueue();
						isAddedToQueue = false;
					}
				}
				else
				{
					targetDestination = targetsGameObjects[0];
					agent.destination = targetDestination.position;
				}
			}
			// End

			//Queue
			if (targetDestination.gameObject.name.Contains("ATM") && !isAddedToQueue && agent.remainingDistance <= 5)
			{
				if (queueControl.atmUsers.Count > 6)
				{
					GoToExit();
					isExiting = true;
				}
				else
				{
					queueControl.AddToQueue(agent.gameObject);
					isAddedToQueue = true;
				}
			}

			atmUserPosition = queueControl.atmUsers.ToArray().ToList().IndexOf(agent.gameObject) + 1;

			if (targetDestination.gameObject.name.Contains("ATM") && atmUserPosition > 1)
			{
				if (agent.remainingDistance <= (atmUserPosition * 2))
				{
					StopAnimation();
				}
				else
				{
					StartAnimation();
				}
			}
			else
			{
			//End Queue


				// Check if we've reached the destination
				// https://answers.unity.com/questions/324589/how-can-i-tell-when-a-navmesh-has-reached-its-dest.html
				// https://stackoverflow.com/questions/60810676/unity-navmesh-event-on-navigation-end
				if (agent.remainingDistance <= 1)
				{
					
					animator.runtimeAnimatorController = idle;
					StartCoroutine(stopSomeSeconds());

					if (targetExists)
					{
						if (numberOfTargets != 0 && currentTarget + 1 < numberOfTargets)
						{
							currentTarget++;

							targetDestination = targetsGameObjects[currentTarget];
							targetDestination = targetsGameObjects[currentTarget].transform;
							agent.SetDestination(targetDestination.position);
						}
						else
						{
							GoToExit();
							isExiting = true;
						}

						agent.SetDestination(targetDestination.position);
					}
					else
					{
						DestroyAgent(agent);
					}

				}

				if (slowOrFaster)
				{
					agent.speed = startSpeed;
				}
			}
		}
		else
		{
			StopAnimation();
		}
		
	}

	IEnumerator stopSomeSeconds()
	{
		StopAnimation();
		isWaiting = true;
		yield return new WaitForSeconds(2);
		//Queue
		if (agent.gameObject.name.Contains("ATM"))
		{
			queueControl.RemoveFromQueue();
		}
		//End Queue
		StartAnimation();
		isWaiting = false;
	}

	void StopAnimation()
	{
		agent.isStopped = true;
		animator.runtimeAnimatorController = idle;
	}
	void StartAnimation()
	{
		agent.isStopped = false;
		animator.runtimeAnimatorController = movement;
	}

	void GoToExit()
	{
		targetExists = false;
		exitGameObjects.Remove(startPoint);
		targetDestination = exitGameObjects[UnityEngine.Random.Range(0, exitGameObjects.Count)];
		agent.SetDestination(targetDestination.position);
	}

	public void DestroyAgent(NavMeshAgent nmAgent)
	{
		Destroy(nmAgent.gameObject, 1);
		nmAgent.gameObject.SetActive(false);
		removeCharacters.RemoveCharacterFromList(nmAgent);
	}
}