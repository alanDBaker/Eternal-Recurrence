using UnityEngine;

public class FollowObject : MonoBehaviour
 {
 	public Vector2 Offset;
 	public Transform Following;
    public Vector3 offSetRotation;

    public void Start()
    {
        transform.Rotate(offSetRotation);
    }

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
