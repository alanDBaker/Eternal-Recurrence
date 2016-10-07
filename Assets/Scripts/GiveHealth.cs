using UnityEngine;
using System.Collections;

public class GiveHealth : MonoBehaviour, IPlayerRespawnListener 
{
	public GameObject Effect;

	public int HealthToGive;

	public void OnTriggerEnter2D(Collider2D other)
	{
		var player = other.GetComponent<Player>();

		gameObject.SetActive(false);

		if (player == null)
			return;

		player.GiveHealth(HealthToGive, gameObject);

		//Instantiate(Effect, transform.position, transform.rotation);
	}

	public void OnPlayerRespawnInThisCheckpoint(CheckPoint checkpoint, Player player)
	{
		gameObject.SetActive(true);
	} 

}
