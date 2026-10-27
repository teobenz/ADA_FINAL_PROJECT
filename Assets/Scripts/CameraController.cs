using UnityEngine;

public class CameraController : MonoBehaviour
{
    private InputScript inputScript;
    private float rotation = 0;

    public LayerMask physicsLayer;

    private void Start()
    {
        inputScript = transform.parent.GetComponent<InputScript>();
    }

    private void Update()
    {

        rotation += -inputScript.lookDir[1] * 0.1f;
        transform.rotation = Quaternion.Euler(rotation, transform.parent.eulerAngles.y, 0);

        if (inputScript.grabPressed)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2, physicsLayer))
            {
                if (hit.transform.gameObject.TryGetComponent<GrabScript>(out GrabScript grab))
                {
                    grab.grab(transform.GetChild(0).gameObject);
                }
            }
        }
    }
}
