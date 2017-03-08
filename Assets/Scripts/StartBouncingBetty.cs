using UnityEngine;

public class StartBouncingBetty : MonoBehaviour
{
    private Animator bouncingBettyAnimator;

    // Use this for initialization
    void Start()
    {
        bouncingBettyAnimator = GetComponent<Animator>();     
        bouncingBettyAnimator.enabled = false;
    }

     public void OnTriggerEnter2D(Collider2D other)
    {
        string text = other.tag;

        if (text == "Player")
        {
            bouncingBettyAnimator.enabled = true;
          
            if (gameObject.name == "bouncing betty" || gameObject.name == "bouncing betty 2" || gameObject.name == "bouncing betty 3")
            {
                Destroy(gameObject, 3);
            }
            
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
