using System.Collections;
using UnityEngine;

public class TimeManipulationTutorial : MonoBehaviour
{
    public InputScript inputScript;

    private bool reverseDone = false;
    private bool advanceDone = false;
    private bool resumeDone = false;

    public TextDisplay reverse;
    public TextDisplay advance;
    public TextDisplay resume;

    private bool started = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(startDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (!started) return;

        if (reverse.activated)
        {
            if (inputScript.timeDir < 0)
            {
                reverse.conditionMet = true;
                reverseDone = true;
            }
        }
        if (advance.activated)
        {
            if (inputScript.timeDir > 0)
            {
                advance.conditionMet = true;
                advanceDone = true;
            }
        }
        if (resume.activated)
        {
            if (inputScript.timeResumed)
            {
                resume.conditionMet = true;
                resumeDone = true;
            }
        }

        if (!reverseDone)
        {
            reverse.activated = true;
        } else if (!advanceDone && reverse.isDone())
        {
            advance.activated = true;
        } else if (!resumeDone && advance.isDone())
        {
            resume.activated = true;
        }
    }

    IEnumerator startDelay()
    {
        float timer = 0;

        while (timer < 15)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        started = true;
    }
}
