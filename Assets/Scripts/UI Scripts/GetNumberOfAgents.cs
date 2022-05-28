using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetNumberOfAgents : MonoBehaviour
{
    private Text numberOfAgentsText;
    private float numberOfAgents;

    void Start()
    {
        numberOfAgents = 0f;
        numberOfAgentsText = this.GetComponent<Text>();
    }

    void Update()
    {
        numberOfAgents = (GameObject.FindGameObjectsWithTag("Character").Length) / 2;
        numberOfAgentsText.text = numberOfAgents.ToString();
    }
}
