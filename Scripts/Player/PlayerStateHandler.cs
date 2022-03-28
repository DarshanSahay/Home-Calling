using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHandler : GenericSingleton<PlayerStateHandler>
{
    public PlayerIdleState idleState;
    public PlayerMoveState moveState;
    public PlayerCombatState combatState;
    public PlayerDeathState deathState;

    [SerializeField]  private PlayerStates initialState;
    [HideInInspector] public PlayerStates activeState;
    [HideInInspector] public PlayerState currentState;

    private void Start()
    {
        InitializeState();
    }
    private void InitializeState()
    {
        switch (initialState)
        {
            case PlayerStates.Idle:
                {
                    currentState = idleState;
                    break;
                }
            case PlayerStates.Move:
                {
                    currentState = moveState;
                    break;
                }
            case PlayerStates.Combat:
                {
                    currentState = combatState;
                    break;
                }
            case PlayerStates.Death:
                {
                    currentState = deathState;
                    break;
                }
            default:
                {
                    currentState = null;
                    break;
                }
        }
        currentState.OnStateEnter();
    }
}
