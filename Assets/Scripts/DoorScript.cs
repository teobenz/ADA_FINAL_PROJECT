using UnityEditor;
using UnityEngine;

public class DoorScript : MonoBehaviour, iActivatable
{
    public GameObject connectedInteractable;
    private iInteractable interactable;

    public Animator[] animators;

    public bool manipulating { get; set; }

    public float normalisedTime { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manipulating = false;
        normalisedTime = 0;

        if (connectedInteractable == null)
        {
            Debug.LogError("no connected interactable found, destroying door script on " + gameObject.name);
            Destroy(this);
        }

        if (connectedInteractable.TryGetComponent<iInteractable>(out iInteractable i))
        {
            interactable = i;
        } else
        {
            Debug.LogError("no interactable script found on " + connectedInteractable);
            Destroy(this);
        }

        for (int j = 0; j < animators.Length; j++)
        {
            animators[j].speed = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!manipulating)
        {
            if (interactable.activated)
            {
                normalisedTime += 0.02f;

                if (normalisedTime > 1)
                {
                    normalisedTime = 1;
                }
            }
            else
            {
                normalisedTime -= 0.02f;

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
