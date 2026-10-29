using UnityEditor;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject connectedInteractable;
    private iInteractable interactable;

    Transform leftDoor;
    Transform rightDoor;

    public bool manipulating = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (connectedInteractable == null)
        {
            Debug.LogError("no connected interactable found, destroying door script on " + gameObject.name);
            Destroy(this);
        }

        if (connectedInteractable.TryGetComponent<iInteractable>(out iInteractable i))
        {
            interactable = i;
        } else
        {
            Debug.LogError("no interactable script found on " + connectedInteractable);
            Destroy(this);
        }

        leftDoor = transform.GetChild(0);
        rightDoor = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!manipulating)
        {
            if (interactable.activated)
            {
                leftDoor.localPosition -= new Vector3(0.01f, 0, 0);
                rightDoor.localPosition += new Vector3(0.01f, 0, 0);

                if (leftDoor.localPosition.x < -1.9f || rightDoor.localPosition.x > 1.9f)
                {
                    leftDoor.localPosition = new Vector3(-1.9f, 2, 0);
                    rightDoor.localPosition = new Vector3(1.9f, 2, 0);
                }
            }
            else
            {
                leftDoor.localPosition += new Vector3(0.01f, 0, 0);
                rightDoor.localPosition -= new Vector3(0.01f, 0, 0);

                if (leftDoor.localPosition.x > -0.75f || rightDoor.localPosition.x < 0.75f)
                {
                    leftDoor.localPosition = new Vector3(-0.75f, 2, 0);
                    rightDoor.localPosition = new Vector3(0.75f, 2, 0);
                }
            }
        }
    }
}
