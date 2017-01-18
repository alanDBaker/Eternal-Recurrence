using UnityEngine;
using System.Collections;

public class GiveHealth : MonoBehaviour, IPlayerRespawnListener 
{
	//public GameObject Effect;

	public int HealthToGive;

	public void OnTriggerEnter2D(Collider2D other)
	{
		var player = other.GetComponent<Player>();
        //Debug.Log(other.name);


		if (player == null)
			return;

    	gameObject.SetActive(false);


        //Debug.Log(other.name);

		player.GiveHealth(HealthToGive, gameObject);

		//Instantiate(Effect, transform.position, transform.rotation);
	}

	public void OnPlayerRespawnInThisCheckpoint(CheckPoint checkpoint, Player player)
	{
		gameObject.SetActive(true);
	} 

}
