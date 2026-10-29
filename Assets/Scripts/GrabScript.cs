using UnityEngine;
using UnityEngine.LowLevelPhysics2D;

public class GrabScript : MonoBehaviour
{
    private InputScript inputScript;

    public GameObject grabbedObject;
    GameObject grabPoint;
    Rigidbody grabbedRb;

    public LayerMask physicsLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputScript = transform.parent.GetComponent<InputScript>();
        grabPoint = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputScript.timeDir != 0)
        {
            grabbedObject = null;
        } else if (inputScript.timeResumed)
        {
            grabbedObject = null;
        }

        if (inputScript.grabPressed)
        {
            if (grabbedObject != null)
            {
                grabbedObject = null;
            }
            else
            {
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2.5f, physicsLayer))
                {
                    if (hit.transform.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
                    {
                        grabbedObject = hit.transform.gameObject;
                        grabbedRb = rb;
                    }
                }
            }
        }

        if (grabbedObject != null)
        {
            grabbedRb.linearVelocity = grabbedRb.linearVelocity * 0.5f;
            grabbedRb.linearVelocity += (grabPoint.transform.position - grabbedObject.transform.position).normalized * (grabPoint.transform.position - grabbedObject.transform.position).magnitude*2;
        }
    }
}
