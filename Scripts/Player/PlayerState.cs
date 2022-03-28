using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    protected PlayerStateHandler statehandler;

    protected virtual void Awake()
    {
        statehandler = GetComponent<PlayerStateHandler>();
    }
    public virtual void OnStateEnter()
    {
        this.enabled = true;
    }

    public virtual void OnStateExit()
    {
        this.enabled = false;
    }
    public void ChangeState(PlayerState newState)
    {
        if (statehandler.currentState != null)
        {
            statehandler.currentState.OnStateExit();
        }

        statehandler.currentState = newState;
        statehandler.currentState.OnStateEnter();
    }
}
