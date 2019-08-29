using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCountdownTimer", menuName = "CountdownTimer")]
public class CountdownTimer : ScriptableObject
{
    public float initialValue;

    float currentValue;
    bool stopped;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="timeFromLastTick">time that passed since last Substraction</param>
    public void Tick(float timeFromLastTick)
    {
        if(!stopped)
            currentValue -= timeFromLastTick;

        //autostop
        if (currentValue <= 0)
            Stop();
    }

    public void Start()
    {

        stopped = false;
        currentValue = initialValue;
    }
    public void Stop()
    {
        stopped = true;
        currentValue = 0;
    }

    public bool Expired { get { return currentValue <= 0; } }
}
