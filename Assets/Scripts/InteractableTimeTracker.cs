using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static PhysicsTimeTracker;

public class InteractableTimeTracker : MonoBehaviour, iTimeTrack
{
    private bool manipulating = false;

    private Queue<data> dataQueue = new Queue<data>();
    private List<data> dataList = new List<data>();
    int listPos;

    public struct data
    {
        public Vector3 position;
        public bool activated;

        public data(Vector3 pos, bool act)
        {
            position = pos;
            activated = act;
        }
    }

    iInteractable interactionScript;

    private InputScript inputScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputScript = GameObject.Find("Player").GetComponent<InputScript>();
        interactionScript = GetComponent<iInteractable>();
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
            data currData = new data(transform.position, interactionScript.activated);

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

        transform.position = dataList[listPos].position;
        interactionScript.activated = dataList[listPos].activated;
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
        interactionScript.activated = dataList[listPos].activated;
    }

    public void Manipulate()
    {
        manipulating = true;
        interactionScript.manipulating = true;

        while (dataQueue.Count != 0)
        {
            dataList.Add(dataQueue.Dequeue());
        }

        listPos = dataList.Count - 1;
    }

    public void Resume()
    {
        manipulating = false;
        interactionScript.manipulating = false;

        for (int i = 0; i < listPos; i++)
        {
            dataQueue.Enqueue(dataList[i]);
        }

        dataList.Clear();
    }
}
