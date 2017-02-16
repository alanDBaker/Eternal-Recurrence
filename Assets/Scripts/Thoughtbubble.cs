using System;
using UnityEngine;
using System.Collections;

public class Thoughtbubble : MonoBehaviour 
{
    private bool wasOpened = false;


	void Start()
	{
		GetComponent<Renderer>().enabled = false;
	}

    public bool isOpen()
    {
        if (wasOpened)
            return true;
        else
            return false;
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Player")
			GetComponent<Renderer>().enabled = true;

        wasOpened = true;

        Debug.Log(wasOpened);
    }

	void OnTriggerExit2D(Collider2D other)
	{
		//Debug.Log(other.name);

		if (other.name == "Player")
			GetComponent<Renderer>().enabled = false;
	}

    
}
