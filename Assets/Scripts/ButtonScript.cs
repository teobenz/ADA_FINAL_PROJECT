using System.Collections;
using UnityEngine;

public class ButtonScript : MonoBehaviour, iInteractable
{
    public bool manipulating { get; set; }
    public bool activated { get; set; }

    Transform child;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manipulating = false;
        activated = false;

        child = transform.GetChild(0);
    }

    private void Update()
    {
        if (child.localPosition.y < 0 && activated == false)
        {
            child.localPosition += new Vector3(0, 0.01f, 0);
        }
    }

    public void Interact()
    {
        activated = !activated;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PhysicsObject"))
        {
            child.position -= new Vector3(0, 0.01f, 0);
            if (child.localPosition.y < -0.15f)
            {
                child.localPosition = new Vector3(0, -0.15f, 0);
            }
            activated = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PhysicsObject"))
        {
            activated = false;
        }
    }
}
