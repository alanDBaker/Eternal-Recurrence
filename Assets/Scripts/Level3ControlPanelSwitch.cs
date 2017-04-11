using UnityEngine;
using System.Collections;

public class Level3ControlPanelSwitch : MonoBehaviour
{
    private Animator theControlPanelSwitchAnimator;
    private GameObject theDoor;
    //private GameObject theDoor2;

    Player player { get; set; }
    

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Player>();

        theDoor = GameObject.Find("Last Door");
     //   theDoor2 = GameObject.Find("Last Door 2");
        theControlPanelSwitchAnimator = GetComponent<Animator>();      
        theControlPanelSwitchAnimator.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        string text = other.tag;

        if (text == "Player")
        {
            theControlPanelSwitchAnimator.enabled = true;
            Destroy(theDoor);
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
