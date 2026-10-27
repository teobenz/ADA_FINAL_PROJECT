using UnityEngine;

public class GrabScript : MonoBehaviour
{
    private bool grabbed = false;

    GameObject grabPoint;
    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbed)
        {
            rb.linearVelocity = rb.linearVelocity * 0.5f;
            rb.linearVelocity += (grabPoint.transform.position - transform.position).normalized * (grabPoint.transform.position - transform.position).magnitude*2;
        }
    }

    public void grab(GameObject point)
    {
        grabPoint = point;
        grabbed = !grabbed;
    }

    public void unGrab()
    {
        grabbed = false;
    }
}
