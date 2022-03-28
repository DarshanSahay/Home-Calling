using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : GenericSingleton<PlayerInputHandler>
{
    void Update()
    {
        GetPlayerMoveInput();
        GetPlayerAttackInput();
    }
    public bool GetPlayerMoveInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if(horizontal != 0 || vertical != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool GetPlayerAttackInput()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
