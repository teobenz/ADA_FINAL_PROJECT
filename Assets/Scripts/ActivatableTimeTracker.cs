using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Analytics.IAnalytic;

public class ActivatableTimeTracker : MonoBehaviour, iTimeTrack
{

    private bool manipulating = false;

    private Queue<data> dataQueue = new Queue<data>();
    private List<data> dataList = new List<data>();
    int listPos;

    public struct data
    {
        
        public float normalisedTime;


        public data(float norms)
        {
            normalisedTime = norms;
        }
    }

    iActivatable activatableScript;

    private InputScript inputScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputScript = GameObject.Find("Player").GetComponent<InputScript>();
        activatableScript = GetComponent<iActivatable>();
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
            data currData = new data(activatableScript.normalisedTime);

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

        if (listPos > 0)
        {
            listPos--;
        }

        activatableScript.normalisedTime = dataList[listPos].normalisedTime;
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

        activatableScript.normalisedTime = dataList[listPos].normalisedTime;
    }

    public void Manipulate()
    {
        manipulating = true;
        activatableScript.manipulating = true;

        while (dataQueue.Count != 0)
        {
            dataList.Add(dataQueue.Dequeue());
        }

        listPos = dataList.Count - 1;
    }

    public void Resume()
    {
        manipulating = false;
        activatableScript.manipulating = false;

        for (int i = 0; i < listPos; i++)
        {
            dataQueue.Enqueue(dataList[i]);
        }

        dataList.Clear();
    }
}
