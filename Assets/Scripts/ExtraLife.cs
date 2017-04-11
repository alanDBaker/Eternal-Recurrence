using UnityEngine;
using System.Collections;

public class ExtraLife : MonoBehaviour
{
    //public GameObject Effect;

    public int ExtraLifeToGive;

    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        //Debug.Log(other.name);

        if (player == null)
            return;

        // makes this gameobject invisible;
        gameObject.SetActive(false);


        //Debug.Log(other.name);

        player.GiveExtraLife(ExtraLifeToGive, gameObject);

        //Instantiate(Effect, transform.position, transform.rotation);
    }

    public void OnPlayerRespawnInThisCheckpoint(CheckPoint checkpoint, Player player)
    {
        gameObject.SetActive(true);
    }

}
