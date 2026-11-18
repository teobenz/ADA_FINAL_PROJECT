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
        transform.rotation = cam.transform.rotation;
        transform.position = cam.transform.position  - transform.forward*0.5f + transform.right*0.5f;        
    }
}
