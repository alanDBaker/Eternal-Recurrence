using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        string text = other.tag;

        //Debug.Log(text);

        if (text == "Player")
            Destroy (GameObject.FindGameObjectWithTag("LairDoor"));
    }
}
