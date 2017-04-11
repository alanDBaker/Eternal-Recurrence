using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, ITakeDamage 
{
	// public varibles will show up on the unity inspector
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
    public AudioClip PlayerExtraLifeSound;
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
    private float movementFactor;
    


    // initialize variables before game Start(); called only once during the lifetime of the script
    public void Awake()
    {
        // cache local variables
        _controller = GetComponent<CharacterController2D>();

        // checks the orientation of the character; local transform
        _isFacingRight = transform.localScale.x > 0;

        // assign full health
		Health = MaxHealth;
    }

    // Called every frame, if monoBehavior is enabled
    public void Update()
    {
    	// subtract from can fire by time; so there will be a fire rate
		_canFireIn -= Time.deltaTime;

		// make sure the player is not dead
		if (! IsDead)
			HandleInput();

		// if player is on the ground the speed factor is different compared to in the air	
         movementFactor = _controller.State.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;

        // if not dead, set the linearly interpolates from a to b by time
        if (IsDead)
			_controller.SetHorizontalForce(0);
        else 
		_controller.SetHorizontalForce(Mathf.Lerp(_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movementFactor));

        // params 1: name of the parameter; param2: the new value for the parameter
        Animator.SetBool("IsGrounded", _controller.State.IsGrounded);

		//Animator.SetBool ("IsDead", IsDead);
		Animator.SetFloat("Speed", Mathf.Abs(_controller.Velocity.x) / MaxSpeed);
    }

    private void HandleInput()
    {
        var value = Input.GetAxis("Horizontal");
        // Debug.Log(value);    

        if (value > 0)
        {
            _normalizedHorizontalSpeed = 1;

            if (!_isFacingRight)
                Flip();
        }
        else if (value < 0)
        {
            _normalizedHorizontalSpeed = -1;

            if (_isFacingRight)
                Flip();
        }
        else
        {
            _normalizedHorizontalSpeed = 0;
        }

        Debug.Log(_isFacingRight);

        if (_controller.CanJump && Input.GetKeyDown("joystick button 0"))
        {
            _controller.Jump();
            AudioSource.PlayClipAtPoint(JumpSound, transform.position);
        }

        if (Input.GetKeyDown("joystick button 2") && SceneManager.GetActiveScene().name != "Level 2 Boss")
            FireProjectile();

        if (Input.GetKeyDown("joystick button 1"))
            Animator.SetTrigger("prone");
    }

    private void FireProjectile()
    {
        // make sure the fire rate is higher than 0
        if (_canFireIn > 0)
            return;

        /*if (FireProjectileEffect != null)
        {
            var effect = (GameObject)Instantiate(FireProjectileEffect, ProjectileFireLocation.position, ProjectileFireLocation.rotation);
            effect.transform.parent = transform;
        }*/

        var direction = _isFacingRight ? Vector2.right : -Vector2.right;

        //Debug.Log(_isFacingRight);

        var projectile = (Projectile)Instantiate(Projectile, ProjectileFireLocation.position, ProjectileFireLocation.rotation);

        projectile.Initialize(gameObject, direction, _controller.Velocity);

        _canFireIn = FireRate;

        AudioSource.PlayClipAtPoint(PlayerShootSound, transform.position);

        Animator.SetTrigger("Fire");
    }

    public void FinishLevel()
	{
        // play sounds?
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
		_controller.SetForce(new Vector2(0, 4));
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

        //Debug.Log(Health);
		if (Health <= 0)
			LevelManager.Instance.KillPlayer();
	}

	public void GiveHealth(int health, GameObject instigator)
	{
		AudioSource.PlayClipAtPoint(PlayerHealthSound, transform.position);
		//FloatingText.Show(string.Format("+{0}", health), "PlayerGotHealthText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2f, 60f));
		
		Health = Mathf.Min(Health + health, MaxHealth);
	}

    public void GiveExtraLife(int lives, GameObject instigator)
    {
        AudioSource.PlayClipAtPoint(PlayerExtraLifeSound, transform.position);

        GameManager.Instance.AddLife();
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

		if (text == "LairDoor")
			Destroy(GameObject.FindGameObjectWithTag("LairDoor"));
     
	}

    public void OnTriggerExit2D(Collider2D other)
    {
      // string text = other.tag;

    }

}