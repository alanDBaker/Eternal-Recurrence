using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public string LevelName;

	public void OnTriggerEnter2D(Collider2D other)
	{
		//var text = other.tag;

        if (other.GetComponent<Player>() == null)
            return;

        LevelManager.Instance.GotoNextLevel(LevelName);
	}

}
