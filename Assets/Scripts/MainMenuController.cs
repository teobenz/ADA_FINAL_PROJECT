using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStart()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
