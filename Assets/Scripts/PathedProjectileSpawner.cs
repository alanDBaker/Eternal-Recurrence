using UnityEngine;
using System.Collections;

public class PathedProjectileSpawner : MonoBehaviour
{
    public Transform Destination;
    public PathedProjectile Projectile;

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

        var projectile = (PathedProjectile) Instantiate(Projectile, transform.position, transform.rotation);

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
