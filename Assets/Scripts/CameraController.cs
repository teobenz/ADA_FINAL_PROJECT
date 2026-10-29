using UnityEngine;

public class CameraController : MonoBehaviour
{
    private InputScript inputScript;
    private float rotation = 0;

    private void Start()
    {
        inputScript = transform.parent.GetComponent<InputScript>();
    }

    private void Update()
    {

        rotation += -inputScript.lookDir[1] * 0.1f;

        if (rotation > 80)
        {
            rotation = 80;
        }
        else if (rotation < -70)
        {
            rotation = -70;
        }

        transform.rotation = Quaternion.Euler(rotation, transform.parent.eulerAngles.y, 0);
    }
}
