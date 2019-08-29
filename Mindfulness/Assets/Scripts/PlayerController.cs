using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerController
{
    private bool isFacingRight = true;  // For determining which way the player is currently facing.
    [Range(0, .3f)]
    [SerializeField]
    private float movementSmoothing = .05f;	// How much to smooth out the movement

    [SerializeField]
    float JumpHeight = 600;
    [SerializeField]
    float Speed = 8;
    [SerializeField]
    LayerMask groundLayerMask;

    Rigidbody2D rigidBody;

    [SerializeField]
    float IsGroundedDelayedTime = 0.25f;

    /// <summary>
    /// store jump request for x time even if currently cant jump or is already jumping
    /// </summary>
    [SerializeField]
    public float JumpKeyDownTimer { get; set; } = 0.2f;


    [SerializeField]
    [Range(0, 1)]
    float weakJumpFactor = 0.5f;

    [SerializeField()]
    bool isGrounded = false;
    bool newIsGrounded = false;


    //public bool IsGrounded { get { return isGrounded; } set { isGrounded = value; } }

    bool alreadyJumpedTwice = false;
    public AerialState AerialState;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        Debug.Log($"{Time.time}: IsGrounded:{isGrounded}");
    }

    void Update()
    {
        Vector2 groundBoxPosition = (Vector2)transform.position + new Vector2(0, -0.01f);
        Vector2 groundBoxSize = (Vector2)transform.localScale + new Vector2(-0.02f, 0);

        newIsGrounded = Physics2D.OverlapBox(groundBoxPosition, groundBoxSize, 0, groundLayerMask);

        if (newIsGrounded != isGrounded)
        {
            isGrounded = newIsGrounded;
            //Debug.Log($"{Time.time}: IsGrounded:{isGrounded}");
        }

        if (isGrounded)
        {
            AerialState = AerialState.Grounded;
        }
        else if (rigidBody.velocity.y < 0)
            AerialState = AerialState.Falling;
        else if (AerialState == AerialState.Grounded)
        {
            AerialState = AerialState.FirstJump;
        }
    }

    public void ResetJumpKeyDownTimer()
    {

    }

    public void UpdateAerialState(CountdownTimer jumpResponseCountdownTimer, bool jumpButtonDown, bool jumpButtonUp)
    {
        jumpResponseCountdownTimer.Tick(Time.deltaTime);

        //Skaczemy
        if (AerialState == AerialState.Grounded
            && jumpButtonDown
            //Mozna wykonac drugi skok
            && jumpResponseCountdownTimer.Expired)
        {
            alreadyJumpedTwice = false;

            rigidBody.AddForce(Vector2.up * JumpHeight);
            jumpResponseCountdownTimer.Start();

            //Debug.Log($"{Time.time}: Jump:{rigidBody.velocity}");
        }
        else if (
            (AerialState == AerialState.FirstJump
            || AerialState == AerialState.Falling)
            && jumpButtonDown
            && !alreadyJumpedTwice)
        {
            alreadyJumpedTwice = true;
            AerialState = AerialState.SecondJump;

            //przed skokiem wyzerowanie predkości wznoszenia, żeby drugi skok nie zwielokratniał bieżącej siły skoku
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);

            rigidBody.AddForce(Vector2.up * JumpHeight);
            jumpResponseCountdownTimer.Stop();

            //Debug.Log($"{Time.time}: Jump2:{rigidBody.velocity}");
        }
        //Wyladowal
        else if (AerialState == AerialState.Grounded)
            jumpResponseCountdownTimer.Stop();
    }

    public void Move(float horizontalAxis)
    {
        Vector3 velocity = rigidBody.velocity;
        Vector3 targetVelocity = new Vector2(horizontalAxis * Speed, rigidBody.velocity.y);
        rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, movementSmoothing);
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        isFacingRight = !isFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
