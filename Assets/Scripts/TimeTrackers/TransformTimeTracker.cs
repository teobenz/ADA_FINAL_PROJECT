using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Analytics.IAnalytic;

public class TransformTimeTracker : MonoBehaviour, iTimeTrack
{
    private bool manipulating = false;

    private Queue<data> dataQueue = new Queue<data>();
    private List<data> dataList = new List<data>();
    int listPos;

    public struct data
    {

        public Vector3 position;
        public Vector3 rotation;


        public data(Vector3 pos, Vector3 rot)
        {
            position = pos; 
            rotation = rot;
        }
    }

    iMoveable moveableScript;

    private InputScript inputScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputScript = GameObject.Find("Player").GetComponent<InputScript>();
        moveableScript = GetComponent<iMoveable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputScript.timeDir > 0)
        {
            Forward();
        }
        else if (inputScript.timeDir < 0)
        {
            Reverse();
        }
        if (inputScript.timeResumed && manipulating)
        {
            Resume();
        }

        if (!manipulating)
        {
            data currData = new data(transform.position, transform.rotation.eulerAngles);

            if (dataQueue.Count > 3000)
            {
                dataQueue.Dequeue();
            }
            dataQueue.Enqueue(currData);
        }
    }

    public void Reverse()
    {
        if (!manipulating)
        {
            Manipulate();
        }

        if (dataList.Count == 0)
        {
            return;
        }

        if (listPos > 0)
        {
            listPos--;
        }
        
        transform.position = dataList[listPos].position;
        transform.rotation = Quaternion.Euler(dataList[listPos].rotation);
    }
    public void Forward()
    {
        if (!manipulating)
        {
            Manipulate();
        }

        if (dataList.Count == 0)
        {
            return;
        }

        if (listPos < dataList.Count - 1)
        {
            listPos++;
        }

        transform.position = dataList[listPos].position;
        transform.rotation = Quaternion.Euler(dataList[listPos].rotation);
    }

    public void Manipulate()
    {
        manipulating = true;
        moveableScript.manipulating = true;

        while (dataQueue.Count != 0)
        {
            dataList.Add(dataQueue.Dequeue());
        }

        if (dataList.Count == 0)
        {
            Resume();
            return;
        }

        listPos = dataList.Count - 1;
    }

    public void Resume()
    {
        manipulating = false;
        moveableScript.manipulating = false;

        if (dataList.Count == 0)
        {
            return;
        }

        for (int i = 0; i < listPos; i++)
        {
            dataQueue.Enqueue(dataList[i]);
        }

        dataList.Clear();
    }
}
