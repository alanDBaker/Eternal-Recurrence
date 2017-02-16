using System;
using UnityEngine;
using System.Collections;

public class LadderZone : MonoBehaviour
{
    private Player _thePlayer;


	// Use this for initialization
	void Start ()
    {
        _thePlayer = FindObjectOfType<Player>();
	
	}
	
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.name == "Player")
        {
            _thePlayer.OnLadder = true;
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.name == "Player")
        {
            _thePlayer.OnLadder = false;
        }
    }
}
