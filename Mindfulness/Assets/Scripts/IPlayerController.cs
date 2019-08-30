using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IPlayerController
{
    void UpdateAerialState(CountdownTimer jumpResponseCountdownTimer, bool jumpButtonDown, bool jumpButtonUp);
    void Move(float axisRaw);
    void Use();
    void Drop();
    void RegisterAsAvailableObject(InteractableObject obj);
    void UnregisterAsAvailableObject(InteractableObject obj);
    bool CanPickup { get; }
    void Pickup(InteractableObject obj);
    Transform GetPointToAttach();
}
