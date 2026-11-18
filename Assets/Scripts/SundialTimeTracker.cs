using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SundialTimeTracker : MonoBehaviour
{
    private bool manipulating = false;

    private Queue<data> dataQueue = new Queue<data>();
    private List<data> dataList = new List<data>();
    int listPos;

    public struct data
    {

        public float rotation;


        public data(float rot)
        {
            rotation = rot;
        }
    }

    SundialAnimation sundialAnimation;

    private InputScript inputScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputScript = GameObject.Find("Player").GetComponent<InputScript>();
        sundialAnimation = GetComponent<SundialAnimation>();
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
            sundialAnimation.rotation = 360 * dataQueue.Count / 3000;
            data currData = new data(sundialAnimation.rotation);

            if (dataQueue.Count < 3000)
            {
                dataQueue.Enqueue(currData);
            }
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

        sundialAnimation.rotation = dataList[listPos].rotation;
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

        sundialAnimation.rotation = dataList[listPos].rotation;
    }

    public void Manipulate()
    {
        manipulating = true;
        sundialAnimation.manipulating = true;

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
        sundialAnimation.manipulating = false;

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
