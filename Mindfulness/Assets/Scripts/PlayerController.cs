using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerController
{
    [SerializeField]
    float JumpHeight = 15;
    [SerializeField]
    float Decelleration = 3;
    [SerializeField]
    LayerMask groundLayerMask;

    Rigidbody2D rigidBody;

    float groundedDelayCountdown = 0;
    [SerializeField]
    float IsGroundedDelayedTime = 0.25f;

    float jumpKeyDownCountdown = 0;
    /// <summary>
    /// store jump request for x time even if currently cant jump or is already jumping
    /// </summary>
    [SerializeField]
    public float JumpKeyDownTimer { get; set; } = 0.2f;


    [SerializeField]
    [Range(0, 1)]
    //Horizontal damping when free moving
    float HorDampingBasic = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    //Horizontal damping when stoping
    float HorDampingStopping = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    //Horizontal damping when turning
    float HorDampTurning = 0.5f;

    [SerializeField]
    [Range(0, 1)]
    float weakJumpFactor = 0.5f;

    bool isGrounded = false;
    public bool IsGrounded { get { return isGrounded; } set{ isGrounded = value; } }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Used for jumping
        groundedDelayCountdown -= Time.deltaTime;
        jumpKeyDownCountdown -= Time.deltaTime;

        Vector2 groundBoxPosition = (Vector2)transform.position + new Vector2(0, -0.01f);
        Vector2 groundBoxSize = (Vector2)transform.localScale + new Vector2(-0.02f, 0);
        isGrounded = Physics2D.OverlapBox(groundBoxPosition, groundBoxSize, 0, groundLayerMask);

        if (isGrounded)
            groundedDelayCountdown = IsGroundedDelayedTime;
        
        //Elevate jump power if jumpKeyIsDown
        if ((jumpKeyDownCountdown > 0) && (groundedDelayCountdown > 0))
        {
            jumpKeyDownCountdown = 0;
            groundedDelayCountdown = 0;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, JumpHeight);
        }
    }

    public void ResetJumpKeyDownTimer()
    {
        jumpKeyDownCountdown = JumpKeyDownTimer;
    }

    public void PerformWeakJump()
    {
        if (rigidBody.velocity.y > 0)
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * weakJumpFactor);
    }

    public void MoveHorizontal(float axisRaw)
    {
        float horizontalVelocity = rigidBody.velocity.x;

        horizontalVelocity += axisRaw;

        if (Mathf.Abs(axisRaw) < 0.01f)
            horizontalVelocity *= Mathf.Pow(1 - HorDampingStopping, Time.deltaTime * Decelleration);
        else if (Mathf.Sign(axisRaw) != Mathf.Sign(horizontalVelocity))
            horizontalVelocity *= Mathf.Pow(1 - HorDampTurning, Time.deltaTime * Decelleration);
        else
            horizontalVelocity *= Mathf.Pow(1 - HorDampingBasic, Time.deltaTime * Decelleration);

        rigidBody.velocity = new Vector2(horizontalVelocity, rigidBody.velocity.y);

    }


}
