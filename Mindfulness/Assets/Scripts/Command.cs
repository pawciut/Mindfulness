using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : MonoBehaviour
{
    public abstract void Execute(Animator anim);
}

public class JumpCommand : Command
{
    public override void Execute(Animator anim)
    {
        //TODO:gdy beda animacje
    }
}

public class PlayerJumpCommand : JumpCommand
{
    float jumpDelayCountdown = 0;

    public void SetJumpDownTimer(IPlayerController player)
    {
        player.ResetJumpKeyDownTimer();
    }

    public void Execute(Animator anim, IPlayerController player)
    {
        this.Execute(anim);
        //TODO:gdy beda animacje
        player.PerformWeakJump();
    }
}


public class HorizontalMoveCommand : Command
{
    public override void Execute(Animator anim)
    {
        //TODO:gdy bed aanimacje
    }
}
public class PlayerHorizontalMoveCommand : HorizontalMoveCommand
{
    public void Execute(Animator anim, IPlayerController player, float horizontalAxis)
    {
        this.Execute(anim);
        player.MoveHorizontal(horizontalAxis);
    }
}



