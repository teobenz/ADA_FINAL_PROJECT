using UnityEngine;

public class SundialAnimation : MonoBehaviour
{
    public bool manipulating = false;

    public float rotation = 0;
    public float rotationSpeed = 10f;

    public ParticleSystem particles;

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, rotation, transform.localRotation.eulerAngles.z);

        if (!manipulating)
        {
            if (particles.isPlaying)
            {
                particles.Stop();
            }
        } else
        {
            if (particles.isStopped)
            {
                particles.Play();
            }
        }
    }
}
