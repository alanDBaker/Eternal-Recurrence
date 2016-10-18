using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    void Awake()
    {
        // keep story music going until scene level 1
        DontDestroyOnLoad(transform.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
        
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log(mode);

        if (scene.name == "Level 1")
        {
            Destroy(gameObject);
        }
        if (scene.name == "Level 2")
        {
            Destroy(gameObject);
        }
        if (scene.name == "Level 2 Boss")
        {
            Destroy(gameObject);
        }

    }
}
