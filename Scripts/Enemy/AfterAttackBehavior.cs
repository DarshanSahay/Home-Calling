using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterAttackBehavior : StateMachineBehaviour
{
    private float timebtwAttacks;
    private float attackRange = 2f;
    private Transform player;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timebtwAttacks = 0;
        player = animator.GetComponent<Enemy>().playerTransform;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timebtwAttacks += Time.deltaTime;
        animator.transform.LookAt(player);
        float distance = Vector3.Distance(animator.transform.position, player.position);

        if (timebtwAttacks >= 2)
        {
            animator.SetBool("inCooldown", false);
        }
        if (distance > attackRange)
        {
            animator.SetBool("inRange", false);
        }
        else
        {
            animator.SetBool("inRange", true);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("inCooldown", false);
    }
}
