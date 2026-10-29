using UnityEngine;

public class RespawnerScript : MonoBehaviour
{
    public Transform spawnLocation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PhysicsObject"))
        {
            if (!other.gameObject.GetComponent<PhysicsTimeTracker>().manipulating)
            {
                other.transform.position = spawnLocation.position + new Vector3(0, 5, 0);
                other.gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            }
        }
    }
}
