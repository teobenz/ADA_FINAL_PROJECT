using System.Collections;
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

    private bool recentJump = false;

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
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 1.2f * transform.localScale.y, groundLayer))
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

        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hitGround, 1.5f * transform.localScale.y, groundLayer) && !recentJump && !isGrounded)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (transform.position.y - hitGround.point.y) + 1.2f, transform.position.z);
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
            StartCoroutine(JumpedCoroutine());
        }

        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        rb.linearVelocity += transform.forward * inputScript.moveDir[1] * moveSpeed + transform.right * inputScript.moveDir[0] * moveSpeed;

        rb.angularVelocity = Vector3.zero;

        rotation += inputScript.lookDir[0] * 0.1f;

        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }

    IEnumerator JumpedCoroutine()
    {
        recentJump = true;
        yield return new WaitForSeconds(2);
        recentJump = false;
    }
}
