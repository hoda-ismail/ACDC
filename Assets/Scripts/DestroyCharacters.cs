using UnityEngine;

public class DestroyCharacters : MonoBehaviour
{
    public float bounds = 1f;

    void Update()
    {
        // if out of given bounds then destroy
        if (transform.position.magnitude > bounds) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        Destroy(gameObject);
    }
}
