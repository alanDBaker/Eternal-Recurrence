
using UnityEngine;
using System.Collections;

public class GunTurretShooterAI : MonoBehaviour
{
    public Transform Destination;

    public PathedProjectile Projectile;

    public float Speed = 3;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var projectile = (PathedProjectile)Instantiate(Projectile, transform.position, transform.rotation);

        projectile.Initalize(Destination, Speed);
    }


   



    public void OnDrawGizmos()
    {
        if (Destination == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, Destination.position);
    }
}
