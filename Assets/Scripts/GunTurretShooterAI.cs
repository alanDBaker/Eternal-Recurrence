
using UnityEngine;
using System.Collections;

public class GunTurretShooterAI : MonoBehaviour
{
    public Transform Destination;
    public Transform Destination2;
    public Transform Destination3;

    public PathedProjectile Projectile;
    public PathedProjectile Projectile2;
    public PathedProjectile Projectile3;

    public float Speed = 3;
    public float fireRate = 3;

    private float _nextShotInSeconds;

    // Use this for initialization
    void Start()
    {
        _nextShotInSeconds = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if ((_nextShotInSeconds -= Time.deltaTime) > 0)
            return;

        _nextShotInSeconds = fireRate;

        var projectile = (PathedProjectile)Instantiate(Projectile, transform.position, transform.rotation);
        var projectile2 = (PathedProjectile)Instantiate(Projectile, transform.position, transform.rotation);
        var projectile3 = (PathedProjectile)Instantiate(Projectile, transform.position, transform.rotation);

        projectile.Initalize(Destination, Speed);
        projectile2.Initalize(Destination, Speed);
        projectile3.Initalize(Destination, Speed);

       new  WaitForSeconds(5);
    }

    public void OnDrawGizmos()
    {
        if (Destination == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, Destination.position);
    }
}
