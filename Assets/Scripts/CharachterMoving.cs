using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharachterMoving : MonoBehaviour
{
    public GameObject characterPrefab;
    public GameObject targetModel;

    private const float interval = 3f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnChar",1f,interval);
    }

    private bool IsModelTracked() {
        return targetModel.GetComponentInChildren<MeshRenderer>().enabled;
    }

    private void SpawnChar() {
        if (IsModelTracked()) {
            Spawn();
        }
    }

    public void Spawn() {
        GameObject.Instantiate(characterPrefab, targetModel.transform.position, targetModel.transform.rotation);
    }
}
