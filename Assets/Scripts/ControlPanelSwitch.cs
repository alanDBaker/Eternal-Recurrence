using UnityEngine;

public class ControlPanelSwitch : MonoBehaviour
{
    
    private Animator theControlPanelSwitchAnimator;
    private GameObject theHouseElevator2;
    private FollowPath theHouseElevator2Path;

    // Use this for initialization
    void Start()
    {
        theHouseElevator2 = GameObject.Find("house elevator 2");
        theControlPanelSwitchAnimator = GetComponent<Animator>();
        theHouseElevator2Path = theHouseElevator2.GetComponent<FollowPath>();
        theHouseElevator2Path.enabled = false;
        theControlPanelSwitchAnimator.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        string text = other.tag;

        if (text == "Player")
        {
            theControlPanelSwitchAnimator.enabled = true;
            theHouseElevator2Path.enabled = true;
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
