using UnityEngine;


public class nonShooterEnemyAI : MonoBehaviour
{
	public int MaxHealth = 100;
	public float Speed;
	public float MaxSpeed = 8;
	public float Damage = 25;
	public int Player_BulletDamage = 50;
	public bool IsDead {get; set;}
	public AudioClip EnemyHit;
	public Animator Animator;

	private int Health {get; set;}
	private CharacterController2D _controller;
	private Vector2 _direction;

	// MonoBehavior.Start() is called on the frame when a script is enabled, called once
	public void Start()
	{
		// get local controller
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

		//Animator.SetFloat("Speed", Mathf.Abs(_controller.Velocity.x) / MaxSpeed);
		AudioListener.volume = 1f;
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

            Health -= Player_BulletDamage;
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
