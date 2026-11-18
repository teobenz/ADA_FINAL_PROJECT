using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public WalkController WalkController;
    public CameraController cameraController;
    public InputScript inputScript;

    private GameObject pauseMenu;

    private bool paused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu = transform.GetChild(0).gameObject;
        pauseMenu.SetActive(false);    
    }

    private void Update()
    {
        if (inputScript.escPressed)
        {
            if (paused)
            {
                onResume();
            } else
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                paused = true;
            }
        }

        WalkController.paused = paused;
        cameraController.paused = paused;
    }

    public void onResume()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        paused = false;
    }

    public void onExit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
