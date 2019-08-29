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

public class MoveCommand : Command
{
    public override void Execute(Animator anim)
    {
        //TODO:gdy bed aanimacje
    }
}


public class IdleCommand : Command
{
    public override void Execute(Animator anim)
    {
        //TODO:gdy bed aanimacje
    }
}
