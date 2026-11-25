using UnityEngine;

public class LaserScript : MonoBehaviour
{
    LineRenderer lineRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.SetPosition(0, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 100))
        {
            lineRenderer.SetPosition(1, hit.point);
        }
    }
}
