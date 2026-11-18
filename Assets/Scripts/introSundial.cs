using UnityEngine;

public class introSundial : MonoBehaviour
{
    Pickupable pickupable;

    public GameObject playerSundial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pickupable = GetComponent<Pickupable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pickupable.activated)
        {
            playerSundial.SetActive(true);
        }
    }
}
