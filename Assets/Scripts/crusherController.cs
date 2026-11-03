using System.Collections;
using UnityEngine;

public class crusherController : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 groundPosition;

    public bool manipulating = false;

    public bool activated = false;

    public float startTime = 0;
    public float intervalTime = 2f;

    public float speed = 50f;

    public LayerMask ground;

    public int currentStep = 0;

    public float globalTimer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        if (Physics.Raycast(transform.position, new Vector3(0,-1,0),out RaycastHit hit, 100))
        {
            groundPosition = hit.point;
        } else
        {
            Debug.LogError("no ground below crusher " + gameObject.name);
            Destroy(gameObject);
        }

        StartCoroutine(startTimerCoroutine(0));
    }

    private void Update()
    {
        if (manipulating)
        {
            StopAllCoroutines();
        }
    }

    public void playStartCoroutine(float timer)
    {
        StartCoroutine(startTimerCoroutine(timer));
    }

    public void playCrushCoroutine()
    {
        StartCoroutine(crushCoroutine());
    }

    public void playIntervalCoroutine(float timer)
    {
        StartCoroutine(intervalCoroutine(timer));
    }

    IEnumerator startTimerCoroutine(float timer)
    {
        activated = false;
        currentStep = 0;
        globalTimer = timer;
        while (globalTimer < startTime)
        {
            globalTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(crushCoroutine());
    }

    IEnumerator crushCoroutine()
    {
        globalTimer = 0;
        currentStep = 1;
        activated = true;
        while ((transform.position.y - groundPosition.y) > 0.1f)
        {
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);

            if (transform.position.y < groundPosition.y)
            {
                transform.position = new Vector3(transform.position.x, groundPosition.y, transform.position.z);
            }
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(intervalCoroutine(0f));
    }

    IEnumerator intervalCoroutine(float timer)
    {
        currentStep = 2;
        globalTimer = timer;
        while (globalTimer < intervalTime * (2f / 10f))
        {
            globalTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        activated = false;
        float returnSpeed = (groundPosition - startPosition).magnitude / (intervalTime*6f/10f);
        while ((transform.position - startPosition).magnitude > 0.1f)
        {
            transform.position += new Vector3(0, returnSpeed * Time.deltaTime, 0);
            yield return new WaitForEndOfFrame();
        }
        if (globalTimer < intervalTime * (8f / 10f))
        {
            globalTimer = intervalTime * (8f / 10f);
        }

        while (globalTimer < intervalTime)
        {
            globalTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(crushCoroutine());
    }
}
