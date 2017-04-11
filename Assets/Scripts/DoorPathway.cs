using UnityEngine;

public class DoorPathway : MonoBehaviour
{
    public Transform exit;
    static Transform last;
    private bool _inDoorWay;

    public bool InDoorWay
    {
        get { return _inDoorWay; }
        set { _inDoorWay = value; }
    }

 
    public void SetDoorWayToTrue()
    {
        _inDoorWay = true;
    }

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
