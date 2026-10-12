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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputScript = GetComponent<InputScript>();

        Cursor.lockState = CursorLockMode.Locked;
        rotation = transform.rotation.eulerAngles.y;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, -transform.up, 1.1f * transform.localScale.y, groundLayer))
        {
            isGrounded = true;
        } else
        {
            isGrounded = false;
        }

        if (!isCrouched && inputScript.crouchPressed)
        {
            transform.position += new Vector3(0, -0.5f, 0);
            transform.localScale = new Vector3(1,0.5f,1);
            isCrouched = true;
        } else if (isCrouched && !inputScript.crouchPressed)
        {
            if (!Physics.Raycast(transform.position, transform.up, 1f))
            {
                transform.position += new Vector3(0, 0.5f, 0);
                transform.localScale = Vector3.one;
                isCrouched = false;
            }
        }

        if (isGrounded && inputScript.jumpPressed)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 5f, rb.linearVelocity.z);
        }

        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        rb.linearVelocity += transform.forward * inputScript.moveDir[1] * 10 + transform.right * inputScript.moveDir[0] * 10;

        rb.angularVelocity = Vector3.zero;

        rotation += inputScript.lookDir[0] * 0.1f;

        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }
}
