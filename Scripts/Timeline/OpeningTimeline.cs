using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class OpeningTimeline : MonoBehaviour
{
    public PlayableDirector openingTimeline;
    public Animator lizaAnimator;
    public PlayableDirector endTimeline;
    public EndingTimeline timeline;
    void Update()
    {
        if(openingTimeline.state == PlayState.Playing)
        {
            lizaAnimator.applyRootMotion = true;
        }
        else
        {
            lizaAnimator.applyRootMotion = false;
        }
        if(timeline.enteredTimelineZone == true)
        {
            lizaAnimator.applyRootMotion = true;
        }
    }
}
