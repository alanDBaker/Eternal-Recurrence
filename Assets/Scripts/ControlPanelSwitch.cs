using UnityEngine;

public class ControlPanelSwitch : MonoBehaviour
{
    
    private Animator theControlPanelSwitchAnimator;
    private Animator theFalconFlameAnimator;
    private GameObject theHouseElevator2;
    private FollowPath theHouseElevator2Path;
    private GameObject theFalconFlame;
   

    // Use this for initialization
    void Start()
    {
        theHouseElevator2 = GameObject.Find("house elevator 2");
        theFalconFlame = GameObject.Find("Falcon Flame");

        theControlPanelSwitchAnimator = GetComponent<Animator>();

        theFalconFlameAnimator = theFalconFlame.GetComponent<Animator>();
        theHouseElevator2Path = theHouseElevator2.GetComponent<FollowPath>();

        theHouseElevator2Path.enabled = false;
        theControlPanelSwitchAnimator.enabled = false;
      

        GetComponent<Renderer>().enabled = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        string text = other.tag;

        if (text == "Player")
        {
            theControlPanelSwitchAnimator.enabled = true;
            theHouseElevator2Path.enabled = true;
            theFalconFlameAnimator.enabled = false;
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
