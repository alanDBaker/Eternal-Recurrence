using UnityEngine;
using System.Collections;

public class PictureTrigger : MonoBehaviour
{
    private Animator thePictureAnimator;
    private GameObject theHouseElevator;
    private FollowPath theHouseElevatorPath;


    // Use this for initialization
    void Start()
    {
        theHouseElevator = GameObject.Find("house elevator");
        thePictureAnimator = GetComponent<Animator>();

        theHouseElevatorPath = theHouseElevator.GetComponent<FollowPath>();

        theHouseElevatorPath.enabled = false;

        GetComponent<Renderer>().enabled = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        string text = other.tag;

        if (text == "Player")
        {
            theHouseElevatorPath.enabled = true;
            thePictureAnimator.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            //bouncingBettyAnimator.enabled = false; 
        }
    }
}
