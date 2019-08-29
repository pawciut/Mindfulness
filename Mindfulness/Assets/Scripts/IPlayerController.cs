using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IPlayerController
{
    //AerialState AerialState { get; set; }

    float JumpKeyDownTimer { get; set; }


    void UpdateAerialState(CountdownTimer jumpResponseCountdownTimer, bool jumpButtonDown, bool jumpButtonUp);
    void Move(float axisRaw);
    void ResetJumpKeyDownTimer();
}
