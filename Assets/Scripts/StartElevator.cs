using UnityEngine;

public class StartElevator : MonoBehaviour
{
    private GameObject theElevator;
    private FollowPath thePath;

    void Start()
    {       
        theElevator = GameObject.FindGameObjectWithTag("ElevatorSwitch");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        string text = other.tag;

        //Debug.Log(text);

        if (text == "Player")
        {
           thePath =  theElevator.GetComponent<FollowPath>();
           thePath.enabled = true;            
        }
    }  
}


