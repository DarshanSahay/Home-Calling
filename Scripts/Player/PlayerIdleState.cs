using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        statehandler.activeState = PlayerStates.Idle;
    }
    public override void OnStateExit()
    {
        base.OnStateExit();
    }
    private void Start()
    {
        statehandler = PlayerStateHandler.Instance;
    }
    private void Update()
    {
        CheckInputsOnIdle();
    }
    void CheckInputsOnIdle()
    {
        if (PlayerInputHandler.Instance.GetPlayerMoveInput())
        {
            statehandler.currentState.ChangeState(statehandler.moveState);
        }
        else
        if (PlayerInputHandler.Instance.GetPlayerAttackInput())
        {
            statehandler.currentState.ChangeState(statehandler.combatState);
        }
    }
}
