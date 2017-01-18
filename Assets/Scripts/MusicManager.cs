using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    void Awake()
    {
        gameObject.SetActive(true);
        DontDestroyOnLoad(transform.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
        
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log(scene);
        
        if (scene.name == "Level 1")
        {
            gameObject.SetActive(false);
        }
        else if (scene.name == "Level 2")
        {
            gameObject.SetActive(false);
        }
        if (scene.name == "Level 2 Boss")
        {
            gameObject.SetActive(false);
        }
        if (scene.name == "Story Scene 20")
        {
            gameObject.SetActive(false);
        }
        if (scene.name == "Level 3")
        {
            gameObject.SetActive(false);
        }

    }
}
