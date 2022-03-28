using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehavior : StateMachineBehaviour
{
    private float attackRange = 2f;
    private float time;
    private float timeBtwAttack = 2.76f;
    private Transform player;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<Enemy>().playerTransform;
        time = 0;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time += Time.deltaTime;
        animator.transform.LookAt(player);
        float distance = Vector3.Distance(animator.transform.position, player.position);
        if (time > timeBtwAttack)
        {
            animator.SetBool("isAttacking", false);
            animator.SetBool("inCooldown", true);
            time = 0;
        }
        if (distance > attackRange)
        {
            animator.SetBool("isAttacking", false);
            animator.SetBool("inRange", false);
        }
        else
        {
            animator.SetBool("inRange", true);
        }
    }
}
