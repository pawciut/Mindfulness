using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IPlayerController
{
    bool IsGrounded { get; set; }
    float JumpKeyDownTimer { get; set; }

    void PerformWeakJump();
    void MoveHorizontal(float axisRaw);
    void ResetJumpKeyDownTimer();
}
