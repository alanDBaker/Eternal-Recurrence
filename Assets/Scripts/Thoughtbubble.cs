using UnityEngine;
using System.Collections;

public class Thoughtbubble : MonoBehaviour 
{
	void Start()
	{
		GetComponent<Renderer>().enabled = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Player")
			GetComponent<Renderer>().enabled = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		//Debug.Log(other.name);

		if (other.name == "Player")
			GetComponent<Renderer>().enabled = false;
	}
}
