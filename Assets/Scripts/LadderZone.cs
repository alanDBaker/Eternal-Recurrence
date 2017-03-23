using System;
using UnityEngine;
using System.Collections;

public class LadderZone : MonoBehaviour
{
    private Player playerScript;
    private GameObject player;
    private CharacterController2D playerController;
    private BoxCollider2D playerCollider;
    private GameObject topOfLadderPlatform;

    public GameObject topOfLadder;

    private BoxCollider2D topCollider;
    public Transform bottomOfLadder;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
        topOfLadder = GameObject.Find("top");
        topOfLadderPlatform = GameObject.Find("platformOnTopofLadder");
        //playerScript = player.AddComponent<Player>();

        playerCollider = player.GetComponent<BoxCollider2D>();
        topCollider = topOfLadder.GetComponent<BoxCollider2D>();

        playerController = player.GetComponent<CharacterController2D>();

    }
    
    public void Update()
    {
        if (topCollider.IsTouching(playerCollider))
        {
            Debug.Log(topCollider.IsTouching(playerCollider));
            gameObject.layer = 10;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            playerController.enabled = true;
            topOfLadderPlatform.SetActive(true);            
        }

        

        //Debug.Log(topOfLadder.transform.position.y);
    }   
	
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.name == "Player")
        {
            playerController.enabled = false;
            //playerScript.OnLadder = true;

        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.name == "Player")
        {
            playerController.enabled = true;
            //playerScript.OnLadder = false;
        }
    }
}
