//Created by K The Bunny

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


//This stores the type of buttplug command you would like to use
//
[SerializeField]
public enum RotateTypes
{
    Typical,
    Clockwise,
    CounterClockwise


}

[Serializable]
public class LoveControlBehavior : PlayableBehaviour
{
    [SerializeField, Range(0,1)] public float intensity = 0;
    [SerializeField] public RotateTypes typeOfVibe;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var server = playerData as StartServerProcessAndScan;

        if (server == null)
            return;
        switch (typeOfVibe)
        {
            case RotateTypes.Typical:
            server.ChangeIntenseTimeline(intensity,0);
                break;
            case RotateTypes.Clockwise:
            server.ChangeIntenseTimeline(intensity,1);
                break;
            case RotateTypes.CounterClockwise:
            server.ChangeIntenseTimeline(intensity,0);
                break;
            
            default:
                break;
        }
    }
}
