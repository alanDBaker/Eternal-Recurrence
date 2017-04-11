using UnityEngine;


public class ControlSwitchPanel2 : MonoBehaviour
{
    private Animator theControlPanelSwitchAnimator;
    //private Animator[] theFalconFlameAnimators;
    private GameObject[] theFalconFlames;


    // Use this for initialization
    void Start()
    {
        theFalconFlames = GameObject.FindGameObjectsWithTag("Falcon Flame");
        theControlPanelSwitchAnimator = GetComponent<Animator>();

        theControlPanelSwitchAnimator.enabled = false;

        foreach (GameObject FalconFlame in theFalconFlames)
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
            
            foreach (GameObject FalconFlame in theFalconFlames)
            {
                               
                FalconFlame.GetComponent<Renderer>().enabled = false;
                FalconFlame.SetActive(false);

                //FalconFlame.GetComponent<BoxCollider>.enabled = false;
            }

        }
    }
}
