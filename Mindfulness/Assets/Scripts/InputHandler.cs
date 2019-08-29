using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject actor;
    IPlayerController player;



    public CountdownTimer JumpResponseCountdownTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = actor.GetComponent<PlayerController>();
    }

        bool jumpButtonDown = false;
        bool jumpButtonUp = false;

    // Update is called once per frame
    void Update()
    {
        jumpButtonDown = false;
        jumpButtonUp = false;
        
        
        if (Input.GetButtonDown("Jump") )
        {
            jumpButtonDown = true;
            //Debug.Log($"{Time.time} Jump Btn Down");
        }

        if (Input.GetButtonUp("Jump"))
        {
            jumpButtonUp = true;
        }

        player.UpdateAerialState(JumpResponseCountdownTimer, jumpButtonDown, jumpButtonUp);
        player.Move( Input.GetAxisRaw("Horizontal"));
        
        //Debug.Log($"{Time.time}: HorizontalAxis:{Input.GetAxisRaw("Horizontal")} Jump Btn Down:{jumpButtonDown} Jump Btn Up: {jumpButtonUp}");
    }
}
