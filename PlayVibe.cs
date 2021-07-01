using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;   

public class PlayVibe : MonoBehaviour
{
    public TimelineAsset[] clipPool;
    public PlayableDirector playThis;
    // Start is called before the first frame update
    public void playVibePattern(int clipID)
    {

        playThis.Play(clipPool[clipID]);

    }
}
