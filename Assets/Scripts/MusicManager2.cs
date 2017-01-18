using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager2 : MonoBehaviour
{
    void Awake()
    {
        // keep story music going until scene level 1
        DontDestroyOnLoad(transform.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name);

        if (scene.name == "Level 3")
        {
            gameObject.SetActive(false);
        }
        
    }
}
