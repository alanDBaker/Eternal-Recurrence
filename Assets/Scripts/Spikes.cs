using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour
{
	public void OnTriggerEnter2D(Collider2D other)
	{
		var name = other.name;

		if (name == "Player")
			Destroy(other);
	}
}
