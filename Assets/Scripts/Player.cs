using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, ITakeDamage 
{
	// these varibles will show up on the inspector component
    public float MaxSpeed = 8;
    public float SpeedAccelerationOnGround = 10f;
    public float SpeedAccelerationInAir = 5f;
	public int MaxHealth = 100;
	//public GameObject OuchEffect;
	public Projectile Projectile;
	public float FireRate;
	public Transform ProjectileFireLocation;
	public AudioClip PlayerHitSound;
	public AudioClip PlayerShootSound;
	public AudioClip PlayerHealthSound;
	public AudioClip DeathScream;
	public AudioClip JumpSound;
	public Animator Animator = null;

	// boilerPlate property for the C# 3.0 compiler. 
	public int Health {get; private set;}
	public bool IsDead {get; private set;}

    // varibles for the player
    private bool _isFacingRight;
    private CharacterController2D _controller;
    private float _normalizedHorizontalSpeed;
    private float _canFireIn;


    // initialize variables before game starts; called only once during the lifetime of the script
    public void Awake()
    {
    	// the type of the component to retrieve
        _controller = GetComponent<CharacterController2D>();

        // checks the orientation of the character; local transform
        _isFacingRight = transform.localScale.x > 0;

        //Debug.Log(transform.loc);
        // assign full health
		Health = MaxHealth;
    }

    // Called every frame, if monoBehavior is enabled
    public void Update()
    {
    	// subtract from can fire; so there will be a fire rate
		_canFireIn -= Time.deltaTime;

		// make sure the player is not dead
		if (! IsDead)
			HandleInput();     

		//	if player is on the ground the speed factor is different compared to in the air	
        var movementFactor = _controller.State.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;

        // make the player not be able to move if dead
		if (IsDead)
			_controller.SetHorizontalForce(0);

		// set the linearly interpolates from a to b by time
		_controller.SetHorizontalForce(Mathf.Lerp(_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movementFactor));

        Debug.Log(_controller.CanJump);
        // params 1: name of the parameter; param2: the new value for the parameter
		Animator.SetBool("IsGrounded", _controller.State.IsGrounded);

		//Animator.SetBool ("IsDead", IsDead);
		Animator.SetFloat("Speed", Mathf.Abs(_controller.Velocity.x) / MaxSpeed);
    }

	public void FinishLevel()
	{
        // play sounds
		enabled = false;
		_controller.enabled = false;
		GetComponent<Collider2D>().enabled = false;
	}

	public void Kill()
	{
		AudioSource.PlayClipAtPoint(DeathScream, transform.position);
		_controller.HandleCollisons = false;
		GetComponent<Collider2D>().enabled = false;
		IsDead = true;
		Health = 0;
		_controller.SetForce(new Vector2(0, 10));
	}

	public void RespawnAt(Transform spawnPoint)
	{
		if (! _isFacingRight)
			Flip();

		IsDead = false;
		GetComponent<Collider2D>().enabled = true;
		_controller.HandleCollisons = true;
		Health = MaxHealth;

		transform.position = spawnPoint.position;
	}

	public void TakeDamage(int damage, GameObject instigator)
	{
		AudioSource.PlayClipAtPoint(PlayerHitSound, transform.position);
		//FloatingText.Show(string.Format("-{0}", damage), "PlayerTakeDamageText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2, 50f));
		//Instantiate(OuchEffect, transform.position, transform.rotation);
		Health -= damage;

        Debug.Log(Health);
		if (Health <= 0)
			LevelManager.Instance.KillPlayer();
	}

	public void GiveHealth(int health, GameObject instigator)
	{
		AudioSource.PlayClipAtPoint(PlayerHealthSound, transform.position);
		//FloatingText.Show(string.Format("+{0}", health), "PlayerGotHealthText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2f, 60f));
		
		Health = Mathf.Min(Health + health, MaxHealth);
	}

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _normalizedHorizontalSpeed = 1;

            if (!_isFacingRight)
                Flip();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _normalizedHorizontalSpeed = -1;

            if (_isFacingRight)
                Flip();
        }
        else
        {
            _normalizedHorizontalSpeed = 0;
        }

        if (_controller.CanJump && Input.GetKeyDown(KeyCode.Space))
        {
			//Debug.Log ("I can jump");
            _controller.Jump();
            AudioSource.PlayClipAtPoint(JumpSound, transform.position);
        }

		if (Input.GetKey(KeyCode.UpArrow))
			FireProjectile();
    }

	private void FireProjectile()
	{
		if (_canFireIn > 0)
			return;

        /*if (FireProjectileEffect != null)
        {
            var effect = (GameObject)Instantiate(FireProjectileEffect, ProjectileFireLocation.position, ProjectileFireLocation.rotation);
            effect.transform.parent = transform;
        }*/

		var direction = _isFacingRight ? Vector2.right : -Vector2.right;

		var projectile = (Projectile) Instantiate(Projectile, ProjectileFireLocation.position, ProjectileFireLocation.rotation);

		projectile.Initialize(gameObject, direction, _controller.Velocity);

		//projectile.transform.localScale = new Vector3(_isFacingRight ? 1 : -1, 1, 1);
		_canFireIn = FireRate;

		AudioSource.PlayClipAtPoint(PlayerShootSound, transform.position);

		Animator.SetTrigger("Fire");
	}

    // reverse the localScale.x to either -1 or 1
    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        _isFacingRight = transform.localScale.x > 0;
    }

	public void OnTriggerEnter2D(Collider2D other)
	{
		var text = other.tag;
		//Debug.Log(text);
        
/*
		if (text == "Ladder")
		{
			//_characterController2D.IsOnLadder = true;
			//IsClimbing = true;
		}

		if (text == "SubBossBulletDamage")
		{
			Health = Health - SubBossBulletDamage;

			Debug.Log(Health);

			AudioSource.PlayClipAtPoint(PlayerHitSound, transform.position);

			if (Health <= 0)
			{
				Debug.Log("player is below 0");
				Kill();
				//WaitForSeconds(2);
				AudioSource.PlayClipAtPoint(DeathScream, transform.position);
				Destroy(gameObject);
				//gameObject.SetActive(false);
			}

		}
		
		if (text == "Bullet")
		{
			Health = Health - BulletDamage;

			Debug.Log(Health);

			AudioSource.PlayClipAtPoint(PlayerHitSound, transform.position);

			if (Health <= 0)
			{
				Debug.Log("player is below 0");
				Kill();
				//WaitForSeconds(2);
				AudioSource.PlayClipAtPoint(DeathScream, transform.position);
				Destroy(gameObject);
				//gameObject.SetActive(false);
			}
		}
		/*if (text == "Spikes") 
		{
			Destroy(gameObject);

			gameObject.SetActive (false);
			Kill();
		}

		if (text == "Enemy")
		{
			Destroy(gameObject);
			Kill();
		}*/

		if (text == "LairDoor")
			Destroy(GameObject.FindGameObjectWithTag("LairDoor"));

     
	}
}























/*using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, ITakeDamage 
{
	// Fields
	private bool _isFacingRight;
	private CharacterController2D _characterController2D;
	private float _normalizedHorizontalSpeed;
	private float _canFire;
	private bool ExtraJump = true;

	// interface on the unity inspector
	public int Health {get; private set;}
	public bool IsDead {get; private set;}
	public bool IsClimbing {get; set;}
	public int MaxHealth = 100;
	public float MaxSpeed = 20f;
	public int BulletDamage = 10;
	public int SubBossBulletDamage = 30;
	public int damage;
	public Animator Animator;
	public Projectile Projectile;
	public float FireRate;
	public Transform ProjectileFireLocation;
	public AudioClip PlayerShootSound;
	public AudioClip PlayerHit;
	public AudioClip DeathScream;
	public AudioClip JumpSound;


	// Awake is used to initialize any variable or game state before the game start. Called once
	public void Awake()
	{
		// Returns the component of Type type; if the game object has one attached, null if not
		_characterController2D = GetComponent<CharacterController2D>();

		// Set bool to True if x-axis is greater than zero, meaning character is facing right
		_isFacingRight = transform.localScale.x > 0;

		IsDead = false;
		IsClimbing = false;
		Health = MaxHealth;
	}

	// Update is called every frame
	public void Update()
	{
		ExtraJump = true;
		
		// Set the _velocity.x 
		_characterController2D.SetHorizontalForce(_normalizedHorizontalSpeed * MaxSpeed);

		// decrease the time since the last fired shot 
		_canFire = _canFire - Time.deltaTime;

		// Call the HandleInput function for each frame
		HandleInput();

		// Set the Animator Speed parameter between zero and one every frame
		Animator.SetFloat("Speed", Mathf.Abs(_characterController2D.Velocity.x) / MaxSpeed);

		// Set the Animator IsGrounded parameter 
		Animator.SetBool("IsGrounded", _characterController2D.State.IsGrounded);

		Animator.SetBool("IsDead", IsDead);

		Animator.SetBool("IsClimbing", IsClimbing);
	}
		
	// player interface function
	private void HandleInput()
	{
		// Returns true while the user holds down the key identified by the key
		if (Input.GetKey(KeyCode.RightArrow))
		{
			_normalizedHorizontalSpeed = 1;

			if (! _isFacingRight)
				Flip();
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			_normalizedHorizontalSpeed = -1;

			if ( _isFacingRight)
				Flip();
		}
		else
		{
			_normalizedHorizontalSpeed = 0;
		}
		// Jumping controls
		if (_characterController2D.CanJump && Input.GetKeyDown(KeyCode.Space))
		{
			_characterController2D.Jump();	

			if (Input.GetKeyDown(KeyCode.Space) && ExtraJump == true)
			{
				_characterController2D.Jump();
				Debug.Log(ExtraJump);	
			}
				  

			AudioSource.PlayClipAtPoint (JumpSound, transform.position);

			//ExtraJump = false;
		}

		/*if (_characterController2D.IsOnLadder && Input.GetKeyDown(KeyCode.C))
		{
			_characterController2D.ClimbLatter();
		}

		if (Input.GetKey(KeyCode.UpArrow))
			FireProjectile(); 

		if (Input.GetKeyDown(KeyCode.F))
			Animator.SetTrigger("FireProne");
	}



	private void KillPlayer()
	{
		IsDead = true;
		Health = 0;
		//AudioListener.volume = 4;
		AudioSource.PlayClipAtPoint(DeathScream, transform.position);
	}

	private void FireProjectile()
	{
		// determine if the weapon is cooled down
		if (_canFire > 0)
			return;

		// determine the character direction; -1 or 1 in the x-axis
		var direction = _isFacingRight ? Vector2.right : -Vector2.right;

		// clones the object original and returns the clone; using a typecast for Projectile class
		var projectile = (Projectile) Instantiate(Projectile, ProjectileFireLocation.position, ProjectileFireLocation.rotation);

		// gameObject = that this componet is attached to
		projectile.Initialize(gameObject, direction, _characterController2D.Velocity);

		// set the fireRate
		_canFire = FireRate;

		AudioSource.PlayClipAtPoint (PlayerShootSound, transform.position);

		// set the fire trigger
		Animator.SetTrigger("Fire");
	}

	// turn the character in opposite
	private void Flip()
	{
		// Negate the x axis with a new object on the heap
		transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

		// Double check if facing right; set to true
		_isFacingRight = transform.localScale.x > 0;
	}

	public void GiveHealth(int health, GameObject instigator)
	{
		//AudioSource.PlayClipAtPoint(PlayerHealthSound, transform.position);
		//FloatingText.Show(string.Format("+{0}", health), "PlayerGotHealthText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2f, 60f));
		//Health += health;
		Health = Mathf.Min (Health + health, MaxHealth);
	}

	public void TakeDamage(int damage, GameObject instigator)
	{
		Health -= damage;
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		var text = other.tag;
		//Debug.Log(text);

		if (text == "Ladder")
		{
			//_characterController2D.IsOnLadder = true;
			IsClimbing = true;
		}

		if (text == "SubBossBulletDamage")
		{
			Health = Health - SubBossBulletDamage;

			Debug.Log(Health);

			AudioSource.PlayClipAtPoint(PlayerHit, transform.position);

			if (Health <= 0)
			{
				Debug.Log("player is below 0");
				KillPlayer();
				//WaitForSeconds(2);

				Destroy(gameObject);
				//gameObject.SetActive(false);
			}

		}
		
		if (text == "Bullet")
		{
			Health = Health - BulletDamage;

			Debug.Log(Health);

			AudioSource.PlayClipAtPoint(PlayerHit, transform.position);

			if (_isFacingRight)
			{
				_characterController2D.SetHorizontalForce(888);
			}
			if (Health <= 0)
			{
				Debug.Log("player is below 0");
				KillPlayer();
				//WaitForSeconds(2);

				Destroy(gameObject);
				//gameObject.SetActive(false);
			}
		}
		if (text == "Spikes") 
		{
			Destroy(gameObject);
			gameObject.SetActive (false);
			KillPlayer();
		}

		if (text == "Enemy")
		{
			Destroy(gameObject);
			KillPlayer();
		}

		if (text == "LairDoor")
			Destroy(GameObject.FindGameObjectWithTag("LairDoor"));

		if (text == "Thought_Bubble")
			other.enabled = true;

	}

	public void OnTriggerExit2D(Collider2D other)
	{
		var text = other.tag;

		if (text == "Ladder")
		{
			//_characterController2D.IsOnLadder = false;
			IsClimbing = false;
		}
			
		/*if (text == "Thought_Bubble")
			other.enabled = false;
	}
}*/


