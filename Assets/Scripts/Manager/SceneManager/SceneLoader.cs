using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] 
    private SceneReference _sceneToLoad;

    public void LoadScene()
    {
        GameObject[] objects = FindObjectsOfType<GameObject>();
        // Loop through all game objects and delete them
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
        SceneManager.LoadScene(_sceneToLoad);
    }
}
