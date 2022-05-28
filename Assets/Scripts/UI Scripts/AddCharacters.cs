using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCharacters : MonoBehaviour
{
    public GameObject spawnerObject;

    private Spawner spawner; 
    private Button btn;

    void Start()
    {
        spawner = spawnerObject.GetComponent<Spawner>();
        btn = this.GetComponent<Button>();
        btn.onClick.AddListener(AddCharacter);
    }

    private void AddCharacter()
	{
        spawner.SpawnChar();
    }
}
