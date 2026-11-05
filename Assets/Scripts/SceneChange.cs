using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    Coroutine myCoroutine = null;
    public string nextScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && myCoroutine == null)
        {
            PostProcessingController ppc = other.GetComponent<PostProcessingController>();
            ppc.FadeOut();
            myCoroutine = StartCoroutine(LoadSceneAsyncCoroutine());
        }
    }

    IEnumerator LoadSceneAsyncCoroutine()
    {
        if (PlayerPrefs.HasKey("spawnPosX"))
        {
            PlayerPrefs.DeleteKey("spawnPosX");
        }

        if (PlayerPrefs.HasKey("spawnPosY"))
        {
            PlayerPrefs.DeleteKey("spawnPosY");
        }

        if (PlayerPrefs.HasKey("spawnPosZ"))
        {
            PlayerPrefs.DeleteKey("spawnPosZ");
        }

        if (PlayerPrefs.HasKey("spawnRotation"))
        {
            PlayerPrefs.DeleteKey("spawnRotation");
        }
        yield return new WaitForSeconds(1);

        AsyncOperation operand = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Single);
        operand.allowSceneActivation = false;

        while (operand.progress < 0.9f)
        {
            yield return new WaitForEndOfFrame();
        }

        operand.allowSceneActivation = true;
    }
}
