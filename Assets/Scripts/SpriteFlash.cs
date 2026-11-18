using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpriteFlash : MonoBehaviour
{
    private RawImage image;

    public Sprite defaultSprite;
    public Sprite followingSprite;

    public float delay = 0.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<RawImage>();
        StartCoroutine(StartFrame());
    }

    IEnumerator StartFrame()
    {
        float timer = 0;
        while (timer < delay)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        image.texture = defaultSprite.texture;
        StartCoroutine(SecondFrame());
    }

    IEnumerator SecondFrame()
    {
        float timer = 0;
        while (timer < delay)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        image.texture = followingSprite.texture;
        StartCoroutine(StartFrame());
    }
}
