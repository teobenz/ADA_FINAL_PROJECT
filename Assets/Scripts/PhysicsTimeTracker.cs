using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsTimeTracker : MonoBehaviour, iTimeTrack
{
    private InputScript inputScript;

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
    private List<transformData> dataList = new List<transformData>();
    int listPos;

    public bool manipulating = false;

    private Rigidbody rb;

    public GameObject transparent;
    GameObject instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputScript = GameObject.Find("Player").GetComponent<InputScript>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.WakeUp();
        if (inputScript.timeDir > 0)
        {
            Forward();
        } else if (inputScript.timeDir < 0)
        {
            Reverse();
        }
        if (inputScript.timeResumed && manipulating)
        {
            Resume();
        }

        if (!manipulating)
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
        } 
    }

    public void Reverse()
    {
        if (!manipulating)
        {
            Manipulate();
        }

        if (listPos > 0)
        {
            listPos--;
        }

        transform.position = dataList[listPos].position;
        transform.rotation = dataList[listPos].rotation;
    }

    public void Forward()
    {
        if (!manipulating)
        {
            Manipulate();
        }

        if (listPos < dataList.Count - 1)
        {
            listPos++;
        }

        transform.position = dataList[listPos].position;
        transform.rotation = dataList[listPos].rotation;
    }

    public void Manipulate()
    {
        rb.isKinematic = true;
        manipulating = true;

        while (dataQueue.Count != 0)
        {
            dataList.Add(dataQueue.Dequeue());
        }

        listPos = dataList.Count - 1;
    }

    public void Resume()
    {
        rb.isKinematic = false;
        manipulating = false;

        rb.linearVelocity = dataList[listPos].velocity;
        rb.angularVelocity = dataList[listPos].angularVelocity;

        for (int i = 0; i < listPos; i++)
        {
            dataQueue.Enqueue(dataList[i]);
        }

        dataList.Clear();
    }
}
