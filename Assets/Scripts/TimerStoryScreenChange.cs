using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TimerStoryScreenChange : MonoBehaviour
{
    public string SceneNameChange;

    void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneNameChange);
    }

}
