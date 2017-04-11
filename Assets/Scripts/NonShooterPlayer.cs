using UnityEngine;

public class NonShooterPlayer : MonoBehaviour
{
    // public varibles will show up on the unity inspector
    public float MaxSpeed = 8;
    public float SpeedAccelerationOnGround = 10f;
    public float SpeedAccelerationInAir = 5f;
    public int MaxHealth = 100;
    public AudioClip PlayerHitSound;
    public AudioClip PlayerHealthSound;
    public AudioClip DeathScream;
    public AudioClip JumpSound;
    public Animator Animator = null;

    // boilerPlate property for the C# 3.0 compiler. 
    public int Health { get; private set; }
    public bool IsDead { get; private set; }

    // varibles for the player
    private bool _isFacingRight;
    private CharacterController2D _controller;
    private float _normalizedHorizontalSpeed;
    private float movementFactor;


    // initialize variables before game starts; called only once during the lifetime of the script
    public void Awake()
    {
        // the type of the component to retrieve
        _controller = GetComponent<CharacterController2D>();

        // checks the orientation of the character; local transform
        _isFacingRight = transform.localScale.x > 0;

        // assign full health
        Health = MaxHealth;
    }

    // Called every frame, if monoBehavior is enabled
    public void Update()
    {
        // make sure the player is not dead
        if (!IsDead)
            HandleInput();

        // Debug.Log(_normalizedVerticalSpeed);

        //	if player is on the ground the speed factor is different compared to in the air	
        movementFactor = _controller.State.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;

        // make the player not be able to move if dead
        if (IsDead)
            _controller.SetHorizontalForce(0);

        // set the linearly interpolates from a to b by time
        _controller.SetHorizontalForce(Mathf.Lerp(_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movementFactor));

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
        _controller.SetForce(new Vector2(0, 3));
    }

    public void RespawnAt(Transform spawnPoint)
    {
        if (!_isFacingRight)
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
            _controller.Jump();
            AudioSource.PlayClipAtPoint(JumpSound, transform.position);
        }

        if (Input.GetKey(KeyCode.DownArrow))
            Animator.SetTrigger("prone");
    }

    // reverse the localScale.x to either -1 or 1
    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        _isFacingRight = transform.localScale.x > 0;
    }


    public void OnTriggerEnter2D(Collider2D other)
    {

    }

    public void OnTriggerExit2D(Collider2D other)
    {
       
    }

}
