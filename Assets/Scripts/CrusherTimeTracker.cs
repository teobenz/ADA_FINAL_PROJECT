using System.Collections.Generic;
using UnityEngine;

public class CrusherTimeTracker : MonoBehaviour, iTimeTrack
{

    private bool manipulating = false;

    private Queue<data> dataQueue = new Queue<data>();
    private List<data> dataList = new List<data>();
    int listPos;

    public struct data
    {
        public int step;
        public float timer;
        public bool activated;
        public Vector3 position;


        public data(int s, float t, bool a, Vector3 p)
        {
            step = s;
            timer = t;
            activated = a;
            position = p;
        }
    }

    crusherController crusherScript;

    private InputScript inputScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputScript = GameObject.Find("Player").GetComponent<InputScript>();
        crusherScript = GetComponent<crusherController>();
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
            data currData = new data(crusherScript.currentStep, crusherScript.globalTimer, crusherScript.activated, crusherScript.transform.position);

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

        crusherScript.activated = dataList[listPos].activated;
        crusherScript.transform.position = dataList[listPos].position;
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

        crusherScript.activated = dataList[listPos].activated;
        crusherScript.transform.position = dataList[listPos].position;
    }

    public void Manipulate()
    {
        manipulating = true;
        crusherScript.manipulating = true;

        while (dataQueue.Count != 0)
        {
            dataList.Add(dataQueue.Dequeue());
        }

        listPos = dataList.Count - 1;
    }

    public void Resume()
    {
        manipulating = false;
        crusherScript.manipulating = false;

        if (dataList[listPos].step == 0)
        {
            crusherScript.playStartCoroutine(dataList[listPos].timer);
        }
        else if (dataList[listPos].step == 1)
        {
            crusherScript.playCrushCoroutine();
        } else
        {
            crusherScript.playIntervalCoroutine(dataList[listPos].timer);
        }

        for (int i = 0; i < listPos; i++)
        {
            dataQueue.Enqueue(dataList[i]);
        }

        dataList.Clear();
    }
}
