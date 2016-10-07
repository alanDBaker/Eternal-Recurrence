using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour
 {
 	public Vector2 Offset;
 	public Transform Following;


    // Update is called once per frame
    void Update()
    {
        if (Following == null)
        {
            Destroy(gameObject);
            return;
        }
            

        transform.position = Following.transform.position + (Vector3)Offset;
    }

}
