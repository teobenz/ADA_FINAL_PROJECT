using UnityEngine;

public class GrabTutorial : MonoBehaviour
{

    public InputScript inputScript;

    public TextDisplay grab;

    public Transform Cam;
    public LayerMask pickupable;

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(Cam.position, Cam.forward, 5, pickupable))
        {
            if (inputScript.grabPressed)
            {
                grab.conditionMet = true;
            }

            grab.activated = true;
        }
    }
}
