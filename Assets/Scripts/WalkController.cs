using UnityEngine;
using UnityEngine.InputSystem;

public class WalkController : MonoBehaviour
{
    private InputScript inputScript;

    private float rotation;

    Rigidbody rb;

    private bool isCrouched = false;
    private bool isGrounded = false;

    public LayerMask groundLayer;

    float moveSpeed = 5f;

    GrabScript grabScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grabScript = transform.GetChild(0).gameObject.GetComponent<GrabScript>();
        inputScript = GetComponent<InputScript>();

        Cursor.lockState = CursorLockMode.Locked;
        rotation = transform.rotation.eulerAngles.y;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 1.1f * transform.localScale.y, groundLayer))
        {
            if (hit.collider.gameObject == grabScript.grabbedObject)
            {
                grabScript.grabbedObject = null;
            }
            isGrounded = true;
        } else
        {
            isGrounded = false;
        }

        if (!isCrouched && inputScript.crouchPressed)
        {
            transform.position += new Vector3(0, -0.5f, 0);
            transform.localScale = new Vector3(1, 0.5f, 1);
            moveSpeed /= 2;
            isCrouched = true;
        }
        else if (isCrouched && !inputScript.crouchPressed)
        {
            if (!Physics.Raycast(transform.position, transform.up, 1f))
            {
                transform.position += new Vector3(0, 0.5f, 0);
                transform.localScale = Vector3.one;
                moveSpeed *= 2;
                isCrouched = false;
            }
        }

        if (isGrounded && inputScript.jumpPressed)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 5f, rb.linearVelocity.z);
        }

        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        rb.linearVelocity += transform.forward * inputScript.moveDir[1] * moveSpeed + transform.right * inputScript.moveDir[0] * moveSpeed;

        rb.angularVelocity = Vector3.zero;

        rotation += inputScript.lookDir[0] * 0.1f;

        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }
}
