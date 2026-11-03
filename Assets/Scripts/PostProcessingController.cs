using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingController : MonoBehaviour
{
    public Volume volume;

    Coroutine myCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myCoroutine = StartCoroutine(FadeInCoroutine());
    }
    public void FadeOut()
    {
        if (myCoroutine == null)
        {
            myCoroutine = StartCoroutine(FadeOutCoroutine());
        }
    }

    IEnumerator FadeOutCoroutine()
    {
        float fadeValue = 0f;

        if (volume.profile.TryGet<Vignette>(out Vignette v))
        {
            while (fadeValue < 1f)
            {
                fadeValue += 1 * Time.deltaTime;
                v.intensity.value = fadeValue;
                yield return new WaitForEndOfFrame();
            }
        }

        myCoroutine = null;
    }

    IEnumerator FadeInCoroutine()
    {
        float fadeValue = 1f;

        if (volume.profile.TryGet<Vignette>(out Vignette v))
        {
            while (fadeValue > 0f)
            {
                fadeValue -= 1 * Time.deltaTime;
                v.intensity.value = fadeValue;
                yield return new WaitForEndOfFrame();
            }

            v.intensity.value = 0f;
        }

        myCoroutine = null;
    }
}
