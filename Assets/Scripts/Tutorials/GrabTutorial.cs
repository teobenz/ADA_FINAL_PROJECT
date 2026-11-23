using UnityEngine;

public class GrabTutorial : MonoBehaviour
{

    public InputScript inputScript;

    public TextDisplay grab;
    public TextDisplay grab2;

    public Transform Cam;
    public LayerMask pickupable;

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(Cam.position, Cam.forward,out RaycastHit hit, 5, pickupable))
        {
            if (hit.transform.gameObject.name.Equals("Sundial"))
            {
                if (inputScript.grabPressed)
                {
                    grab.conditionMet = true;
                }
                grab.activated = true;
            } else
            {
                if (inputScript.grabPressed)
                {
                    grab2.conditionMet = true;
                }
                grab2.activated = true;
            } 
        }
    }
}
