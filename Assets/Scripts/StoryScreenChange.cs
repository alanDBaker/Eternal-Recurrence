using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryScreenChange : MonoBehaviour
{

    public string SceneNameChange;

    // Update is called once per frame
    void Update()
    {
        if (! Input.GetKey(KeyCode.Space))
            return;

        SceneManager.LoadScene(SceneNameChange);

    }
}
