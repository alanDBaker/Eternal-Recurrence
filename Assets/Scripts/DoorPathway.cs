using UnityEngine;

public class DoorPathway : MonoBehaviour
{
    public Transform exit;
    static Transform last;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (exit == last)
                return;

        TeleportToExit(other);
    }

    void OnTriggerExit2D()
    {
        if (exit == last)
                last = null;
    }

    void TeleportToExit(Collider2D other)
    {
        last = transform;
        other.transform.position = exit.transform.position;
    }
    
}
