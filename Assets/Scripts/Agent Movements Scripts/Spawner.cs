using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//[RequireComponent(typeof(NavMeshAgent))]

public class Spawner : MonoBehaviour
{
	// This is ModelTarget object added from Vuforia menu
	public GameObject targetModel;

	// Time interval in seconds to spawn cars
	public float interval = 5f;

	// Possible characters to spawn
	public List<GameObject> charGameObjects = new List<GameObject>();

	// Possible starting points
	public List<Transform> startGameObjects = new List<Transform>();

	public GameObject startButton;

	public GameObject queueControlGameObject;

	// Transform agentPosition;
	GameObject character;

	// Transform startPosition;
	Transform startPosition;

	private AgentCharacteristics agentCharacteristics;
	private ScaleNavigator scaleNavigator;
	private PlayPauseIconChanger playPauseIconChanger;
	//private QueueControl queueControl;


	// Start is called before the first frame update
	void Start()
	{
		//InvokeRepeating("SpawnChar", 1f, interval);
		//queueControl = queueControlGameObject.GetComponent<QueueControl>();
		playPauseIconChanger = startButton.GetComponent<PlayPauseIconChanger>();
}

	private void Update()
	{
		/*if (IsModelTracked() && startButton.transform.transform.tag == "Pause" && playPauseIconChanger.fistTimeStart)
		{
			for (int i = 0; i < 10; i++)
			{
				SpawnChar();
			}
		}*/
	}

	private bool IsModelTracked()
	{
		return targetModel.GetComponentInChildren<MeshRenderer>().enabled;
	}

	public void SpawnChar()
	{
		if (IsModelTracked() && startButton.transform.transform.tag == "Pause")
		{
			Spawn();
		}
	}

	public void Spawn()
	{
		character = charGameObjects[Random.Range(0, charGameObjects.Count)];
		startPosition = startGameObjects[Random.Range(0, startGameObjects.Count)];
		var iniCharacter = GameObject.Instantiate(character, startPosition.position, startPosition.rotation);
		iniCharacter.transform.parent = targetModel.transform;
		agentCharacteristics = iniCharacter.GetComponent<AgentCharacteristics>();
		agentCharacteristics.startPoint = startPosition;
		agentCharacteristics.queueControlGameObject = queueControlGameObject;
		iniCharacter.SetActive(true);

		var lowScaleCharacter = GameObject.Instantiate(iniCharacter, startPosition.position, startPosition.transform.rotation);
		Destroy(lowScaleCharacter.GetComponent<AgentCharacteristics>());
		Destroy(lowScaleCharacter.GetComponent<NavMeshAgent>());
		lowScaleCharacter.transform.parent = targetModel.transform;
		lowScaleCharacter.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
		foreach (Transform trans in lowScaleCharacter.GetComponentsInChildren<Transform>(true))
		{
			trans.gameObject.layer = 0;
		}

		scaleNavigator = lowScaleCharacter.AddComponent<ScaleNavigator>();
		scaleNavigator.bigScaleObject = iniCharacter;
	}
}
