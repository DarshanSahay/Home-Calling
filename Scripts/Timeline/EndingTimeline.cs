using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EndingTimeline : MonoBehaviour
{
    public float endTime = 46f;
    public PlayableDirector endTimeline;
    public LevelLoader endLoading;
    public Animator lizaAnimator;
    public AudioSource source;
    public bool enteredTimelineZone = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerHealth>() != null)
        {
            enteredTimelineZone = true;
            lizaAnimator.applyRootMotion = true;
            endTimeline.Play();
        }
    }
    private void Update()
    {
        if(enteredTimelineZone == true)
        {
            endTime -= Time.deltaTime;
            if (endTime <= 0)
            {
                endLoading.LoadLevel(1);
                endTime = 46f;
                enteredTimelineZone = false;
            }
        }
        if (endTimeline.state == PlayState.Playing)
        {
            lizaAnimator.applyRootMotion = true;
            source.Stop();
        }
        else
        {
            lizaAnimator.applyRootMotion = false;
        }
    }
}
