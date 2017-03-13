using UnityEngine;
using System.Collections;

public class CheckBatteries : MonoBehaviour 
{

    GameObject batt1, batt2;
    public string LevelName;

	// Use this for initialization
	void Start() 
    {
        batt1 = GameObject.Find("Battery");
        batt2 = GameObject.Find("Battery 2");

	}
	
	// Update is called once per frame
	void Update()
    {
	    if (batt1 == null && batt2 == null)
        {

            LevelManager.Instance.GotoNextLevel(LevelName);
        }
	}
}
