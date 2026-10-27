using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DesaturationScript : MonoBehaviour
{
    private PlayerInput playerInput;

    private InputAction power;
    private InputAction look;
    private InputAction grab;

    public Volume vol;

    private float rotation = 0;

    ColorAdjustments colorAdjustments;

    bool isTimeStopped = false;
    bool hasRun = true;

    private Coroutine myCoroutine;

    public PhysicsTimeTracker[] timeObject;

    public GameObject grabPoint;

    public LayerMask noPlayerLayer;
    GameObject target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        power = playerInput.actions.FindAction("Power");
        look = playerInput.actions.FindAction("Look");
        grab = playerInput.actions.FindAction("Grab");

        vol.profile.TryGet<ColorAdjustments>(out colorAdjustments);
    }

    // Update is called once per frame
    void Update()
    {
        if (grab.WasPressedThisFrame())
        {
            if (target == null)
            {
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;


                if (Physics.Raycast(ray, out hit, 10, noPlayerLayer))
                {
                    if (hit.collider.CompareTag("Physics"))
                    {
                        target = hit.collider.gameObject;
                        target.GetComponent<GrabScript>().grab(grabPoint);
                    }
                }
            } else
            {
                target.GetComponent<GrabScript>().grab(grabPoint);
                target = null;
            }
        }

        if (power.WasPressedThisFrame() && hasRun)
        {
            isTimeStopped = !isTimeStopped;
            hasRun = false;
        }

        //if (isTimeStopped && !hasRun && myCoroutine == null)
        //{
        //    myCoroutine = StartCoroutine(SaturationCoroutine(-1));
        //    for (int i = 0; i < timeObject.Length; i++)
        //    {
        //        timeObject[i].reversing = true;
        //    }
        //}
        if (!isTimeStopped && !hasRun && myCoroutine == null)
        {
            myCoroutine = StartCoroutine(SaturationCoroutine(1));
        }

        rotation -= look.ReadValue<Vector2>()[1] * 0.1f;

        if (rotation > 90)
        {
            rotation = 90;
        } else if (rotation < -90)
        {
            rotation = -90;
        }

            transform.rotation = Quaternion.Euler(rotation, transform.parent.rotation.eulerAngles.y, 0);
    }

    IEnumerator SaturationCoroutine(float change)
    {
        int max = 100;
        for (int i = 0; i < max; i++)
        {
            yield return new WaitForSeconds(1 / max);
            colorAdjustments.saturation.value += change / max * 100;
        }

        hasRun = true;
        myCoroutine = null;
    }
}
