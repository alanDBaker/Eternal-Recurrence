using UnityEngine;
using System.Collections;

public class StartTheGunTurret : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D other)
    {
        string text = other.tag;

        //Debug.Log(text);

        if (text == "Player")
        {
            
        }
            
    }
}

