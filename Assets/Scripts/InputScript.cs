using UnityEngine;
using UnityEngine.InputSystem;

public class InputScript : MonoBehaviour
{
    PlayerInput playerInput;

    InputAction move;
    InputAction look;
    InputAction jump;
    InputAction grab;
    InputAction crouch;
    InputAction time;
    InputAction timeResume;
    InputAction esc;

    public Vector2 moveDir = Vector2.zero;
    public Vector2 lookDir = Vector2.zero;
    public bool jumpPressed = false;
    public bool grabPressed = false;
    public bool crouchPressed = false;
    public float timeDir = 0;
    public bool timeResumed = false;
    public bool escPressed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        move = playerInput.actions.FindAction("Move");
        look = playerInput.actions.FindAction("Look");
        jump = playerInput.actions.FindAction("Jump");
        grab = playerInput.actions.FindAction("Grab");
        crouch = playerInput.actions.FindAction("Crouch");
        time = playerInput.actions.FindAction("Time");
        timeResume = playerInput.actions.FindAction("TimeResume");
        esc = playerInput.actions.FindAction("Escape");

    }

    // Update is called once per frame
    void Update()
    {
        moveDir = move.ReadValue<Vector2>();
        lookDir = look.ReadValue<Vector2>();

        if (jump.WasPressedThisFrame())
        {
            jumpPressed = true;
        }
        if (jump.WasReleasedThisFrame())
        {
            jumpPressed = false;
        }

        grabPressed = grab.WasPressedThisFrame();

        if (crouch.WasPressedThisFrame())
        {
            crouchPressed = true;
        }
        if (crouch.WasReleasedThisFrame())
        {
            crouchPressed = false;
        }

        timeDir = time.ReadValue<float>();

        timeResumed = timeResume.WasPressedThisFrame();

        escPressed = esc.WasPressedThisFrame();
    }
}
