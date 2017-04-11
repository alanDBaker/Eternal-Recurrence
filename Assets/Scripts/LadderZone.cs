using UnityEngine;

public class LadderZone : MonoBehaviour
{
    private Player playerScript;
    private GameObject player;
    private CharacterController2D playerController;
    private BoxCollider2D playerCollider;
    private GameObject topOfLadderPlatform;

    public GameObject topOfLadder;

    private BoxCollider2D topCollider;
    private BoxCollider2D bottomCollider;
    public GameObject bottomOfLadder;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");

        topOfLadder = GameObject.Find("top");
        bottomOfLadder = GameObject.Find("bottom");

        topOfLadderPlatform = GameObject.Find("platformOnTopofLadder");

        playerCollider = player.GetComponent<BoxCollider2D>();

        topCollider = topOfLadder.GetComponent<BoxCollider2D>();
        bottomCollider = bottomOfLadder.GetComponent<BoxCollider2D>();

        playerController = player.GetComponent<CharacterController2D>();

    }
    
    public void Update()
    {
        if (topCollider.IsTouching(playerCollider))
        {
            //Debug.Log(topCollider.IsTouching(playerCollider));
            gameObject.layer = 10;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            playerController.enabled = true;
            topOfLadderPlatform.SetActive(true);            
        }

        else if (bottomCollider.IsTouching(playerCollider))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            playerController.enabled = true;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

        if (playerCollider.IsTouching(bottomCollider))
        {

        }
        

        //Debug.Log(topOfLadder.transform.position.y);
    }   
	
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.name == "Player")
        {
           playerController.enabled = false;
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.name == "Player")
        {
            playerController.enabled = true;
        }
    }
}
