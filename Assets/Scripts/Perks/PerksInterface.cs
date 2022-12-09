using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public abstract class PerksInterface
{
    public float duration;
    public float timerAction;
    public float timerCooling;
    public float cooldown;
    public bool isCooling;
    public bool isTriggered;

    public void Action(bool trigger)
    {
        if (trigger && !isTriggered && !isCooling)
            isTriggered = true;
        if (isTriggered && timerAction <= duration)
        {
            timerAction += Time.deltaTime;
            Code();
        }
        else
        {
            if (isTriggered)
            {
                isCooling = true;
                isTriggered = false;
                timerAction = 0;
            }
            if (isCooling && timerCooling <= cooldown)
                timerCooling += Time.deltaTime;
            else
            {
                isCooling = false;
                timerCooling = 0;
            }
        }
    }

    //
    public abstract void Code();
}