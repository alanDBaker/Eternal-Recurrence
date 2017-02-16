using UnityEngine;


public class GunTurretShooterAI : MonoBehaviour
{
    public Transform ProjectileFireLocation;
    public float FireRate = 1;
    public float _distanceToPlayer = 30;
    public Projectile Projectile;
    public AudioClip ShootSound;

    private CharacterController2D _controller;
    //private GameObject _playerPosition;
    //private Vector3 _playerDirection;
    public Vector3 _direction;
    private float _canFireIn;

    // MonoBehavior.Start() is called on the frame when a script is enabled, called once
    public void Start()
    {
        // get local controller
       _controller = GetComponent<CharacterController2D>();

      // _playerPosition = GameObject.Find("Player");

        //_playerPosition.transform.position
        // set the local direction to left
        //_direction = new Vector3(-1, -1, 0);
    }

    // called once per frame
    public void Update()
    {
        // decrease the time clock on the shot fire
        if ((_canFireIn -= Time.deltaTime) > 0)
            return;

       // _playerPosition = GameObject.Find("Player");

     //   _playerDirection = new Vector3(_playerPosition.transform.position.x,_playerPosition.transform.position.y, _playerPosition.transform.position.z);

        // determine if the player is in sight of the enemy
        RaycastHit2D rayCastHit = Physics2D.Raycast(transform.position, _direction, _distanceToPlayer, LayerMask.GetMask("Player"));

        //Debug.DrawLine(transform.position, _direction);

        // exit update() if not hitting a collider masked as player 
        if (!rayCastHit)
            return;

        //Debug.Log(_playerDirection);

        // clone the bullet
        var projectile = (Projectile)Instantiate(Projectile, ProjectileFireLocation.position, transform.rotation);


        // set up the bullet
        projectile.Initialize(gameObject, _direction, _controller.Velocity);

        // set the fireRate
        _canFireIn = FireRate;

        AudioListener.volume = 1f;
        AudioSource.PlayClipAtPoint(ShootSound, transform.position);
    }

    public void TakeDamage(int damage, GameObject instigator)
    {
       // AudioSource.PlayClipAtPoint(EnemyHit, transform.position);

    }

    // should not be used inside update
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + _direction * _distanceToPlayer);
    }


    // does the enemy run into a platform to turn around
    public void OnTriggerEnter2D(Collider2D other)
    {

    }
}

