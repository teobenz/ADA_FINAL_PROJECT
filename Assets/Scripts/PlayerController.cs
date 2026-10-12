    using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    PlayerInput playerInput;

    InputAction move;
    InputAction jump;

    public float speed;
    public float maxSpeed;

    public GameObject look;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("no Rigidbody");
            Destroy(gameObject);
        }
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError("no playerInput");
            Destroy(gameObject);
        }

        move = playerInput.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity += transform.forward * move.ReadValue<Vector2>().y * speed + transform.right * move.ReadValue<Vector2>().x * speed;

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            Vector3 normal = rb.linearVelocity.normalized;
            rb.linearVelocity = normal * maxSpeed;
        }

        Vector3 movement = new Vector3(rb.linearVelocity.x, 0,rb.linearVelocity.z);

        look.transform.position = transform.position + movement.normalized * 5;

        if (movement.magnitude < 0.1f)
        {
            look.transform.position = transform.forward * 5;
        }

        transform.LookAt(look.transform);
    }
}
