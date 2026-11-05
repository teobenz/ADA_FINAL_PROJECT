using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("spawnPosX", other.transform.position.x);
            PlayerPrefs.SetFloat("spawnPosY", other.transform.position.y);
            PlayerPrefs.SetFloat("spawnPosZ", other.transform.position.z);
            PlayerPrefs.SetFloat("spawnRotation", transform.eulerAngles.y);
        }
    }
}
