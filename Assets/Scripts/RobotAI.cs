using UnityEngine;
using System.Collections;

public class RobotAI : MonoBehaviour
{
    public int MaxHealth = 100;
    public Transform ProjectileFireLocation;
    public float Speed;
    public float MaxSpeed = 8;
    public float FireRate = 1;
    public float _distanceToPlayer = 20;
    public int Player_BulletDamage = 50;
    public bool IsDead { get; set; }
    public Projectile Projectile;
    public AudioClip ShootSound;
    public AudioClip EnemyHit;
    public Animator Animator;

    public int Health { get; set; }
    private CharacterController2D _controller;
    public Vector3 DirectionToPlayer;
    private Vector3 _direction;
    private float _canFireIn;

    // MonoBehavior.Start() is called on the frame when a script is enabled, called once
    public void Start()
    {
        // get local controller
        _controller = GetComponent<CharacterController2D>();

        // set the local direction to left
        _direction = new Vector3(-1, 0, 0);

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
        RaycastHit2D rayCastHit = Physics2D.Raycast(transform.position, DirectionToPlayer, _distanceToPlayer, LayerMask.GetMask("Player"));

        // exit update() if not hitting a collider masked as player 
        if (!rayCastHit)
            return;

        // clone the enemy bullet
        var projectile = (Projectile)Instantiate(Projectile, ProjectileFireLocation.position, transform.rotation);

        // set up the bullet
        projectile.Initialize(gameObject, DirectionToPlayer, _controller.Velocity);

        // set the fireRate
        _canFireIn = FireRate;

        //Debug.Log(24332);

        //Debug.Log(Mathf.Abs(_controller.Velocity.x) / MaxSpeed);

        //Animator.SetFloat("Speed", Mathf.Abs(_controller.Velocity.x) / MaxSpeed);
        //AudioListener.volume = 1f;
        AudioSource.PlayClipAtPoint(ShootSound, transform.position);
    }

    public void TakeDamage(int damage, GameObject instigator)
    {
        AudioSource.PlayClipAtPoint(EnemyHit, transform.position);
        //FloatingText.Show(string.Format("-{0}", damage), "PlayerTakeDamageText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2, 50f));
        //Instantiate(OuchEffect, transform.position, transform.rotation);
        Health -= damage;

        Debug.Log(Health);
        if (Health <= 0)
            LevelManager.Instance.KillPlayer();
    }

    // should not be used inside update
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + DirectionToPlayer * _distanceToPlayer);
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
            //Debug.Log(Health);

            Health -= Player_BulletDamage;
            //Destroy(other);
            //AudioListener.volume = 5f;
            AudioSource.PlayClipAtPoint(EnemyHit, transform.position);

            if (Health <= 0)
            {
                //Debug.Log("health is below 0");

                // kill the enemy object
                Destroy(gameObject);
            }
            //Debug.Log(PlayerDamage);

        }
        //Debug.Log(text);
    }
}
