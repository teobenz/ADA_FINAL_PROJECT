using UnityEngine;

public class Pickupable : MonoBehaviour, iInteractable
{
    public bool manipulating {  get; set; }

    public bool activated { get; set; }

    public GameObject[] activatableObjects;

    private void Start()
    {
        activated = false;
    }

    public void Interact()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        BoxCollider collider = GetComponent<BoxCollider>();

        Destroy(mesh);
        Destroy(collider);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        activated = true;

        for (int i = 0; i < activatableObjects.Length; i++)
        {
            activatableObjects[i].AddComponent<ActivatableTimeTracker>();
        }
    }
}
