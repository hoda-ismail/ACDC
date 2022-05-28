using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RemoveCharacters : MonoBehaviour
{
    private Button btn;
    private NavMeshAgent agentToRemove;
    private List<NavMeshAgent> agents;

    void Start()
    {
        agents = new List<NavMeshAgent>();
        btn = this.GetComponent<Button>();
        btn.onClick.AddListener(RemoveCharacter);
}
    private void RemoveCharacter()
    {
        if (agents.Count > 0)
        {
            agentToRemove = agents[(Random.Range(0, agents.Count))];

            Destroy(agentToRemove.gameObject, 1);
            agentToRemove.gameObject.SetActive(false);
            RemoveCharacterFromList(agentToRemove);
        }
    }

    public void AddCharacter(NavMeshAgent agent)
    {
        agents.Add(agent);
    }

    public void RemoveCharacterFromList(NavMeshAgent agent)
    {
        agents.Remove(agent);
    }


}
