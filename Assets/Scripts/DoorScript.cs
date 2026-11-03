using UnityEditor;
using UnityEngine;

public class DoorScript : MonoBehaviour, iActivatable
{
    public GameObject[] connectedInteractables;
    private iInteractable[] interactables;

    public Animator[] animators;
    public bool manipulating { get; set; }

    public float normalisedTime { get; set; }

    public float animationSpeed = 1f;

    public int requiredCount = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manipulating = false;
        normalisedTime = 0;

        if (connectedInteractables == null)
        {
            Debug.LogError("no connected interactable found, destroying door script on " + gameObject.name);
            Destroy(this);
        }

        interactables = new iInteractable[connectedInteractables.Length];
        for (int i = 0; i < connectedInteractables.Length; i++)
        {
            if (connectedInteractables[i].TryGetComponent<iInteractable>(out iInteractable interactable))
            {
                interactables[i] = interactable;
            }
            else
            {
                Debug.LogError("no interactable script found on " + connectedInteractables[i]);
                Destroy(this);
            }
        }

        for (int j = 0; j < animators.Length; j++)
        {
            animators[j].speed = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int count = 0;
        if (!manipulating)
        {
            for (int i = 0; i < interactables.Length; i++)
            {
                if (interactables[i].activated)
                {
                    count++;
                }
            }

            if (count == requiredCount)
            {
                normalisedTime += animationSpeed * Time.deltaTime;

                if (normalisedTime > 1)
                {
                    normalisedTime = 1;
                }
            }
            else
            {
                normalisedTime -= animationSpeed * Time.deltaTime;

                if (normalisedTime < 0)
                {
                    normalisedTime = 0;
                }
            }
        }

        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].Play(animators[i].GetCurrentAnimatorClipInfo(0)[0].clip.name, 0, normalisedTime);
        }
    }
}
