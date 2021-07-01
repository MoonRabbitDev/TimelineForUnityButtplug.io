using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class LoveControlClip : PlayableAsset, ITimelineClipAsset
{
    [SerializeField]
    private LoveControlBehavior template = new LoveControlBehavior();
    public ClipCaps clipCaps{get{return ClipCaps.Blending;}}

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<LoveControlBehavior>.Create(graph, template);
    }


}
