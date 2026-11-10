using UnityEngine;

public class MovingPlatform : MonoBehaviour, iActivatable
{
    public bool manipulating { get; set; }

    public Transform startPoint;
    public Transform endPoint;

    private Vector3 travel;

    public float startingNormalisedTime = 0;
    public float normalisedTime { get; set; }
    public float speed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manipulating = false;
        normalisedTime = startingNormalisedTime;
        transform.parent = transform.parent.parent;
        travel = endPoint.position - startPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!manipulating)
        {
            normalisedTime += Time.deltaTime * speed;

            if (normalisedTime > 1)
            {
                normalisedTime = 0;
            }
        }

        transform.position = startPoint.position + travel * normalisedTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (collision.transform.parent == transform)
            {
                collision.gameObject.transform.parent = transform.parent;
            }
        }
    }
}
