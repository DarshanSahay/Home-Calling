using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerState
{
    [SerializeField] private PlayerHealth pHealth;
    [SerializeField] private Animator anim;
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        statehandler.activeState = PlayerStates.Death;
        AfterDeath();
    }
    public override void OnStateExit()
    {
        base.OnStateExit();
    }
    private void AfterDeath()
    {
        StartCoroutine(OpenRespawnScreen());
    }
    private IEnumerator OpenRespawnScreen()                              //opens a UI screen after player has died and resets health
    {                                                                    
        UIManager.Instance.OnPlayerDeath();
        anim.Play("Idle");
        yield return new WaitForSeconds(7f);
        pHealth.SetHealth();
        statehandler.currentState.ChangeState(statehandler.idleState);
    }
}
