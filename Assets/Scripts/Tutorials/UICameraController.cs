using UnityEngine;

public class UICameraController : MonoBehaviour
{
    Transform cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main.transform;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cam.transform.position;
        transform.rotation = cam.transform.rotation;
    }
}
