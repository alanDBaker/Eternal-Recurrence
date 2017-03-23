using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    public string LevelName;
    GameObject theBossMan;
    public string alternativeEnding;

    public void Start()
    {
        theBossMan = GameObject.Find("Dr. Otto");
    }

	public void OnTriggerEnter2D(Collider2D other)
	{
		//var text = other.tag;

        if (other.GetComponent<Player>() == null)
            return;

        Debug.Log(theBossMan);

        if (theBossMan != null)
        {
            LevelManager.Instance.GotoNextLevel(alternativeEnding);
        }
        else
        {
            LevelManager.Instance.GotoNextLevel(LevelName);
        }
    }

}
