using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class IntroTutorial : MonoBehaviour
{
    public InputScript inputScript;

    private bool movementDone = false;
    private bool jumpDone = false;

    public TextDisplay movement;
    public TextDisplay jump;

    private bool WPressed = false;
    private bool APressed = false;
    private bool SPressed = false;
    private bool DPressed = false;

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

        if (movement.activated)
        {
            if (inputScript.moveDir[0] > 0)
            {
                DPressed = true;
            } else if (inputScript.moveDir[0] < 0)
            {
                APressed = true;
            }

            if (inputScript.moveDir[1] > 0)
            {
                WPressed = true;
            }
            else if (inputScript.moveDir[1] < 0)
            {
                SPressed = true;
            }

            if (WPressed && SPressed && DPressed && APressed)
            {
                movementDone = true;
                movement.conditionMet = true;
            }
        }
        if (jump.activated)
        {
            if (inputScript.jumpPressed)
            {
                jump.conditionMet = true;
                jumpDone = true;
            }
        }

        if (!movementDone)
        {
            movement.activated = true;
        }
        else if (!jumpDone && movement.isDone())
        {
            jump.activated = true;
        }
    }

    IEnumerator startDelay()
    {
        float timer = 0;

        while (timer < 3)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        started = true;
    }
}
