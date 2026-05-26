using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public static event Action shootBall;
    public static void InvokeShootBall()
    {
        shootBall?.Invoke();
    }

    public static event Action leftFlipperTriggered;
    public static void InvokeLeftFlipperTriggered()
    {
        leftFlipperTriggered?.Invoke();
    }
    
    public static event Action rightFlipperTriggered;
    public static void InvokeRightFlipperTriggered()
    {
        rightFlipperTriggered?.Invoke();
    }

    public static event Action<float> rotationAD;
    public static void InvokeRotationAD(float rotationValue)
    {
        rotationAD?.Invoke(rotationValue);
    }

    public static event Action<float> rotation;
    public static void InvokeRotation(float rotationValue)
    {
        rotation?.Invoke(rotationValue);
    }

    public static event Action<float> machineTilted;
    public static void InvokeMachineTilted(float tiltAmount)
    {
        machineTilted?.Invoke(tiltAmount);
    }

    public static event Action reset;
    public static void InvokeReset()
    {
        reset?.Invoke();
    }
}
