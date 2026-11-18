using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.Port;

public class TextDisplay : MonoBehaviour
{
    public RawImage[] images;

    public bool activated = false;
    public bool conditionMet = false;

    private bool conditionActivated = false;
    private bool hasRun = false;

    public float SecondsPerLetter = 0.1f;
    public float animationTime = 1;

    private bool displayed = false;
    private bool complete = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activated && !hasRun)
        {
            StartCoroutine(DisplayText());
        }

        if (conditionMet && !conditionActivated && displayed)
        {
            StartCoroutine(TextFade());
        }
    }

    public bool isReady()
    {
        return displayed;
    }

    public bool isDone()
    {
        return complete;
    }

    IEnumerator DisplayText()
    {
        hasRun = true;
        for (int i = 0; i < images.Length; i++)
        {
            yield return new WaitForSeconds(SecondsPerLetter);
            images[i].gameObject.SetActive(true);
            StartCoroutine(TextAnimation(images[i]));
        }
        StartCoroutine(displayTimer());
        displayed = true;
    }

    IEnumerator TextAnimation(RawImage image)
    {
        float timer = 0f;
        Color c;

        while (timer < animationTime)
        {
            timer += Time.deltaTime;
            
            float opacity = 2f * timer / animationTime;
            if (opacity > 1f)
            {
                opacity = 1f;
            }

            c = new Color(image.color.r, image.color.g, image.color.b, opacity);
            image.color = c;
            image.transform.localScale = Vector3.one * 2 / (1 + timer / animationTime);

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator TextFade()
    {
        conditionActivated = true;
        float timer = 0;
        Color c;

        while (timer < animationTime)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();

            float opacity = 1 - timer / animationTime;
            if (opacity < 0f)
            {
                opacity = 0f;
            }
            for (int i = 0; i < images.Length; i++)
            {
                c = new Color(images[i].color.r, images[i].color.g, images[i].color.b, opacity);
                images[i].color = c;
            }
        }

        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(false);
        }
        complete = true;
    }

    IEnumerator displayTimer()
    {
        float timer = 0;

        while (timer < 5)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        conditionMet = true;
    }
}
