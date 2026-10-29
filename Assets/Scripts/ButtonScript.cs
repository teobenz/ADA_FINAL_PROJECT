using System.Collections;
using UnityEngine;

public class ButtonScript : MonoBehaviour, iInteractable
{
    public bool manipulating { get; set; }
    public bool activated { get; set; }

    int collisions = 0;
    Coroutine myCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manipulating = false;
        activated = false;
    }

    private void Update()
    {
        if (collisions == 0 && myCoroutine == null)
        {
            activated = false;
        }
        if (transform.localPosition.y < 0 && activated == false && !manipulating)
        {
            transform.localPosition += new Vector3(0, 0.005f, 0);
        }
    }

    public void Interact()
    {
        activated = !activated;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("PhysicsObject"))
        {
            collisions++;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("PhysicsObject"))
        {
            if (!manipulating)
            {
                if (myCoroutine != null)
                {
                    StopCoroutine(myCoroutine);
                }

                transform.position -= new Vector3(0, 0.01f, 0);
                if (transform.localPosition.y < -0.15f)
                {
                    transform.localPosition = new Vector3(0, -0.15f, 0);
                }
                activated = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("PhysicsObject"))
        {
            collisions--;
            if (!manipulating)
            {
                myCoroutine = StartCoroutine(deactivateCoroutine());
            }
        }
    }

    IEnumerator deactivateCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        activated = false;
        myCoroutine = null;
    }
}
