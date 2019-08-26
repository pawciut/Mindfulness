using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject actor;
    IPlayerController player;
    Animator anim;
    PlayerJumpCommand jumpCommand;
    PlayerHorizontalMoveCommand horizontalMoveCommand;



    // Start is called before the first frame update
    void Start()
    {
        jumpCommand = new PlayerJumpCommand();
        horizontalMoveCommand = new PlayerHorizontalMoveCommand();
        anim = actor.GetComponent<Animator>();
        player = actor.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            jumpCommand.SetJumpDownTimer(player);

        if (Input.GetButtonUp("Jump"))
            jumpCommand.Execute(anim, player);

        horizontalMoveCommand.Execute(anim, player, Input.GetAxisRaw("Horizontal"));
    }
}
