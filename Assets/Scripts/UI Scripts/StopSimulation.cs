using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopSimulation : MonoBehaviour
{
    public GameObject stopWatchObject;
    public GameObject playPauseIconChangerObject;

    private Button btn;
    private StopWatch stopWatch;
    private PlayPauseIconChanger playPauseIconChanger;


    void Start()
    {
        stopWatch = stopWatchObject.GetComponent<StopWatch>();
        playPauseIconChanger = playPauseIconChangerObject.GetComponent<PlayPauseIconChanger>();
        
        btn = this.GetComponent<Button>();
        btn.onClick.AddListener(StopAgentSimulation);
    }

    void StopAgentSimulation()
    {
        GameObject[] agents = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject agent in agents)
        GameObject.Destroy(agent);
        stopWatch.ResetTimer();
        playPauseIconChanger.ResetButton();
    }
}
