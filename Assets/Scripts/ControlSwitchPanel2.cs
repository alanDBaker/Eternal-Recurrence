using UnityEngine;


public class ControlSwitchPanel2 : MonoBehaviour
{
    private Animator theControlPanelSwitchAnimator;
    private Animator[] theFalconFlameAnimator;
    private GameObject[] theFalconFlame;


    // Use this for initialization
    void Start()
    {
        theFalconFlame = GameObject.FindGameObjectsWithTag("Falcon Flame");
        theControlPanelSwitchAnimator = GetComponent<Animator>();
        //theFalconFlameAnimator = theFalconFlames
        theControlPanelSwitchAnimator.enabled = false;

        foreach (GameObject FalconFlame in theFalconFlame)
        {
            FalconFlame.GetComponent<Animator>().enabled = true;
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        string text = other.tag;

        if (text == "Player")
        {
            theControlPanelSwitchAnimator.enabled = true;
            
            foreach (GameObject FalconFlame in theFalconFlame)
            {
                FalconFlame.GetComponent<Renderer>().enabled = false;
            }

        }
    }

/*    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            //bouncingBettyAnimator.enabled = false; 
        }
    }
    */
}
