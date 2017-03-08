using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        string text = other.tag;

        //Debug.Log(text);

        if (text == "Player")
            Destroy(GameObject.FindGameObjectWithTag("LairDoor"));

        if (text == "Player")
            Destroy(GameObject.FindGameObjectWithTag("GunShield"));

        if (text == "Player")
            Destroy(GameObject.FindGameObjectWithTag("ElevatorDoor"));

        if (text == "Player")
            Destroy(GameObject.FindGameObjectWithTag("Level 5 Gate"));

    }
}
