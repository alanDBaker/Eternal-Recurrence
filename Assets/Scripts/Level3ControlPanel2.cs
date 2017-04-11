using UnityEngine;

public class Level3ControlPanel2 : MonoBehaviour
{
    private Animator theControlPanelSwitchAnimator;
    private GameObject theDoor2;

    // Use this for initialization
    void Start()
    {
        theDoor2 = GameObject.Find("Last Door 2");
        theControlPanelSwitchAnimator = GetComponent<Animator>();
        theControlPanelSwitchAnimator.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        string text = other.tag;

        if (text == "Player")
        {
            theControlPanelSwitchAnimator.enabled = true;
            Destroy(theDoor2);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
        }
    }
}