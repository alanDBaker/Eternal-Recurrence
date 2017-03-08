using UnityEngine;

public class CommentOnorOff : MonoBehaviour
{
    private GameObject theThoughtBubbleObject;
    private Thoughtbubble theThoughtBubbleScript;

    // Use this for initialization
    void Start()
    {
        //theThoughtBubbleObject = new Thoughtbubble();

        theThoughtBubbleObject = GameObject.FindGameObjectWithTag("TresureSwitch");

        theThoughtBubbleScript =  theThoughtBubbleObject.GetComponent<Thoughtbubble>();

        GetComponent<Renderer>().enabled = false;

        Debug.Log(theThoughtBubbleScript.isOpen());
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        string text = other.tag;

        if (text == "Player")
        {
           if (theThoughtBubbleScript.isOpen())
            {
                GetComponent<Renderer>().enabled = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
          if (other.name == "Player")
            GetComponent<Renderer>().enabled = false;
    }
}
