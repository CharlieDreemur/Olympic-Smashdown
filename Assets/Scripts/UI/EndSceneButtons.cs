using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneButtons : MonoBehaviour
{
    public void GoToMainMenu()
    {
        StartCoroutine(LoadAsyncScene("Scenes/MainMenu"));
    }

    public void GoToFirstLevel()
    {
        StartCoroutine(LoadAsyncScene("Scenes/MainScene"));
    }
    
    public IEnumerator LoadAsyncScene(string targetScene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
