using UnityEngine;
using System.Collections;

public class GateControlSwitch : MonoBehaviour
{
    private Animator theGateSwitchAnimator;

    // Use this for initialization
    void Start()
    {
        theGateSwitchAnimator = GetComponent<Animator>();

        theGateSwitchAnimator.enabled = false;

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        string text = other.tag;

        if (text == "Player")
        {
            theGateSwitchAnimator.enabled = true;
        }
    }
}
