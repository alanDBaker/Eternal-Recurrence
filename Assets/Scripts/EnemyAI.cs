using UnityEngine;


public class EnemyAI : MonoBehaviour, IPlayerRespawnListener
{
    public int MaxHealth = 100;
    public Transform ProjectileFireLocation;
    public float Speed;
    public float FireRate = 1;
    public bool IsDead { get; set; }
    public int PlayerBulletDamage;
    public float distanceToPlayer = 20;
    public Projectile Projectile;
    public AudioClip ShootSound;
    public AudioClip EnemyHit;
    public Animator Animator;
    public AudioClip DeathScream;

    public int Health { get; set; }
    private CharacterController2D _controller;
    private Vector2 _direction;
    private float _canFireIn;

    // MonoBehavior.Start() is called on the frame when a script is enabled, called once
    public void Start()
    {
        // cache local controller
        _controller = GetComponent<CharacterController2D>();

        // set the local direction to right
        _direction = new Vector2(1, 0);

        // bad guy starts off not dead
        IsDead = false;

        // bad guy starts with max health
        Health = MaxHealth;
    }

    // called once per frame
    public void Update()
    {
        // Set the speed on the enemy
        _controller.SetHorizontalForce(_direction.x * Speed);

        // check for condtions if the enemy needs to turn around
        if ((_direction.x < 0 && _controller.State.IsCollidingLeft) || (_direction.x > 0 && _controller.State.IsCollidingRight))
        {
            // reverse the direction on the bad guy
            _direction = -_direction;

            // reverse the direction for the vector
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        // decrease the time clock on the shot fire
        if ((_canFireIn -= Time.deltaTime) > 0)
            return;

        // determine if the player is in sight of the enemy
        var rayCastHit = Physics2D.Raycast(transform.position, _direction, 20, LayerMask.GetMask("Player"));

        // determine if the player is in sight of the enemy
        //RaycastHit2D rayCastHit = Physics2D.Raycast(transform.position, _direction, distanceToPlayer, LayerMask.GetMask("Player"));

        // exit the function if not hitting the player 
        if (! rayCastHit)
            return;

        

        // clone the enemy bullet
        var projectile = (Projectile)Instantiate(Projectile, ProjectileFireLocation.position, transform.rotation);

        // set up the bullet
        projectile.Initialize(gameObject, _direction, _controller.Velocity);

        // set the fireRate
        _canFireIn = FireRate;

        //Debug.Log(24332);

        //Debug.Log(Mathf.Abs(_controller.Velocity.x) / MaxSpeed);

        //Animator.SetFloat("Speed", Mathf.Abs(_controller.Velocity.x) / MaxSpeed);
        AudioListener.volume = 1f;
        AudioSource.PlayClipAtPoint(ShootSound, transform.position);
    }

   /* public void TakeDamage(int damage, GameObject instigator)
    {
        /*if (PointsToGivePlayer != 0)
        {

        }
        //Instantiate(DestroyedEffect, transform.position, transform.rotation);
        Debug.Log("EnemnyAI - damage -" + damage + "instigator" + instigator);

        Health -= damage;

        Debug.Log(Health);
        if (Health <= 0)
            EnemyManager.KillEnemy();
    }*/

    public void Kill()
    {
        Debug.Log(gameObject);

        //AudioSource.PlayClipAtPoint(DeathScream, transform.position);
        /* _controller.HandleCollisons = false;
         GetComponent<Collider2D>().enabled = false;
         IsDead = true;
         Health = 0;
         _controller.SetForce(new Vector2(0, 10));*/
        //Destroy(gameObject);
        GetComponent<Renderer>().enabled = false;
    }

    public void OnPlayerRespawnInThisCheckpoint(CheckPoint checkpoint, Player player)
    {
        /* _direction = new Vector2(-1, 0);
         transform.localScale = new Vector3(1, 1, 1);
         transform.position = _startPosition;
         gameObject.SetActive(true);*/
    }

    // does the enemy run into a platform to turn around
    public void OnTriggerEnter2D(Collider2D other)
    {
        var text = other.tag;
        //Debug.Log(text);

        if (text == "Platform_turnAround")
            _controller.State.IsCollidingLeft = _controller.State.IsCollidingRight = true;


        if (text == "Player_Bullet")
        {
            Debug.Log(Health);

            Health -= PlayerBulletDamage;
            //Destroy(other);
            AudioListener.volume = 5f;
            AudioSource.PlayClipAtPoint(EnemyHit, transform.position);

            if (Health <= 0)
            {
                Debug.Log("health is below 0");
                // kill the enemy object
                Destroy(gameObject);
            }
            //Debug.Log(PlayerDamage);

        }
        //Debug.Log(text);
    }
}

