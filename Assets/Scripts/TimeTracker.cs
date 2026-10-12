using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeTracker : MonoBehaviour
{
    [System.Serializable]
    public struct transformData
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 velocity;
        public Vector3 angularVelocity;

        public transformData(Vector3 pos, Quaternion rot, Vector3 vel, Vector3 angularVel)
        {
            rotation = rot;
            position = pos;
            velocity = vel;
            angularVelocity = angularVel;
        }
    }

    private Queue<transformData> dataQueue = new Queue<transformData>();

    public bool reversing = false;
    private bool isRunning = false;

    private Rigidbody rb;

    public GameObject transparent;
    GameObject instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!reversing)
        {
            transformData data = new transformData(transform.position, transform.rotation, rb.linearVelocity, rb.angularVelocity);

            if (dataQueue.Count == 0)
            {
                if (instance != null)
                {
                    Destroy(instance);
                }
                instance = Instantiate(transparent, data.position, data.rotation);
            }

            if (dataQueue.Count > 3000)
            {
                if (instance != null)
                {
                    Destroy(instance);
                }
                transformData oldData = dataQueue.Dequeue();
                instance = Instantiate(transparent, oldData.position, oldData.rotation);
                dataQueue.Enqueue(data);
            }
            else
            {
                dataQueue.Enqueue(data);
            }
        } else if (!isRunning)
        {
            rb.isKinematic = true;
            StartCoroutine(reverseCoroutine());
        }
    }

    IEnumerator reverseCoroutine()
    {
        isRunning = true;
        transformData[] dataArray = dataQueue.ToArray();
        for (int i = dataQueue.Count - 1; i >= 0; i--)
        {
            transform.position = dataArray[i].position;
            transform.rotation = dataArray[i].rotation;
            yield return new WaitForEndOfFrame();
        }

        dataQueue.Clear();

        rb.isKinematic = false;

        transform.position = dataArray[0].position;
        transform.rotation = dataArray[0].rotation;
        rb.linearVelocity = dataArray[0].velocity;
        rb.angularVelocity = dataArray[0].angularVelocity;

        isRunning = false;
        reversing = false;
    }
}
