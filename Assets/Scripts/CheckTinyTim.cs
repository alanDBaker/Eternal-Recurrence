using UnityEngine;

public class CheckTinyTim : MonoBehaviour
{

    GameObject tinyTim;
    public string LevelName;

    // Use this for initialization
    void Start()
    {
        tinyTim = GameObject.Find("Tiny Tim");

        
    }

    // Update is called once per frame
    void Update()
    {
        if (tinyTim == null)
        {
            Debug.Log(tinyTim);
            LevelManager.Instance.GotoNextLevel(LevelName);
        }
    }
}
