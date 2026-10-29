using System.Collections.Generic;
using UnityEngine;

public class DoorTimeTracker : MonoBehaviour, iTimeTrack
{
    private bool manipulating = false;

    private Queue<data> dataQueue = new Queue<data>();
    private List<data> dataList = new List<data>();
    int listPos;

    public struct data
    {
        public Vector3 leftPosition;
        public Vector3 rightPosition;

        public data(Vector3 leftPos, Vector3 rightPos)
        {
            leftPosition = leftPos;
            rightPosition = rightPos;
        }
    }

    private Transform leftDoor;
    private Transform rightDoor;

    private InputScript inputScript;
    private DoorScript doorScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputScript = GameObject.Find("Player").GetComponent<InputScript>();
        doorScript = GetComponent<DoorScript>();

        leftDoor = transform.GetChild(0);
        rightDoor = transform.GetChild(1);
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
            data currData = new data(leftDoor.position, rightDoor.position);

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

        leftDoor.position = dataList[listPos].leftPosition;
        rightDoor.position = dataList[listPos].rightPosition;
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

        leftDoor.position = dataList[listPos].leftPosition;
        rightDoor.position = dataList[listPos].rightPosition;
    }

    public void Manipulate()
    {
        manipulating = true;
        doorScript.manipulating = true;

        while (dataQueue.Count != 0)
        {
            dataList.Add(dataQueue.Dequeue());
        }

        listPos = dataList.Count - 1;
    }

    public void Resume()
    {
        manipulating = false;
        doorScript.manipulating = false;

        for (int i = 0; i < listPos; i++)
        {
            dataQueue.Enqueue(dataList[i]);
        }

        dataList.Clear();
    }
}
