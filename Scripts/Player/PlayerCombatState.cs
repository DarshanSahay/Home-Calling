using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatState : PlayerState
{
    [SerializeField] private CharacterController charController;
    [SerializeField] private Animator charAnim;
    [SerializeField] private AudioSource attackSound;
    [SerializeField] private AudioSource swordSound;
    [SerializeField] private AudioClip[] attackClips;
    [SerializeField] private AudioClip[] swordClips;
    [SerializeField] private AudioSource charWalk;
    [SerializeField] private AudioSource charRun;

    private bool combo_1Possible;
    private bool combo_2Possible;
    private bool isAttackingCombo1 = false;
    private bool isAttackingCombo2 = false;
    private bool stillGaurding = false;

    private int combo_1Step;                                                      //stores the no. of Clicks user pressed
    private int combo_2Step;
    private float maxCombo_Limit;                                                 //max time limit to perform a combo
    private float gaurdHoldTime;

    private int attack_1_1Hash = Animator.StringToHash("Attack_1_1");             //for performance optimization converting string to int 
    private int attack_1_2Hash = Animator.StringToHash("Attack_1_2");
    private int attack_1_3Hash = Animator.StringToHash("Attack_1_3");

    private int attack_2_1Hash = Animator.StringToHash("Attack_2_1");
    private int attack_2_2Hash = Animator.StringToHash("Attack_2_2");
    private int attack_2_3Hash = Animator.StringToHash("Attack_2_3");
    private int attack_2_4Hash = Animator.StringToHash("Attack_2_4");

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        statehandler.activeState = PlayerStates.Combat;
    }
    public override void OnStateExit()
    {
        base.OnStateExit();
    }
    void Update()
    {
        if (!isAttackingCombo1 && !isAttackingCombo2 && !stillGaurding)
        {
            statehandler.currentState.ChangeState(statehandler.idleState);
        }
        Attack_1Input();
        Attack_2Input();
        Gaurd_Input();
    }
    private void Attack_1Input()                             //getting input for left mouse button 
    {
        if (isAttackingCombo2 == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FirstCombo();
            }
        }
        if (isAttackingCombo1)
        {
            maxCombo_Limit -= Time.deltaTime;
            if (maxCombo_Limit <= 0)
            {
                maxCombo_Limit = 1.5f;
                isAttackingCombo1 = false;
                ResetFirstCombo();
                EnableCharController();
            }
        }
    }
    private void Attack_2Input()                         //getting input for right mouse button 
    {
        if (isAttackingCombo1 == false)
        {
            if (Input.GetMouseButtonDown(1))
            {
                SecondCombo();
            }
        }

        if (isAttackingCombo2)
        {
            maxCombo_Limit -= Time.deltaTime;
            if (maxCombo_Limit <= 0)
            {
                maxCombo_Limit = 1.5f;
                isAttackingCombo2 = false;
                ResetSecondCombo();
                EnableCharController();
            }
        }
    }
    private void Gaurd_Input()                                //taking input for blocking incoming attacks
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            stillGaurding = true;
            gaurdHoldTime += Time.deltaTime;
            charAnim.SetTrigger("canGaurd");
            if (gaurdHoldTime >= 0.33f && stillGaurding)
            {
                charAnim.ResetTrigger("canGaurd");
                charAnim.SetTrigger("canHold");
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            stillGaurding = false;
            charAnim.ResetTrigger("canHold");
            charAnim.SetTrigger("canStopGaurd");
            Invoke("CloseGaurd", 0.83f);
            gaurdHoldTime = 0;
            EnableCharController();
        }
    }
    public void FirstComboPossible()
    {
        combo_1Possible = true;
    }
    public void ResetFirstCombo()
    {
        combo_1Possible = false;
        combo_1Step = 0;
    }
    public void SecondComboPossible()
    {
        combo_2Possible = true;
    }
    public void ResetSecondCombo()
    {
        combo_2Possible = false;
        combo_2Step = 0;
    }
    public void FirstComboAtk()
    {
        maxCombo_Limit = 1.5f;

        if (combo_1Step == 2)
        {
            isAttackingCombo1 = true;
            charAnim.Play(attack_1_2Hash);
        }
        if (combo_1Step == 3)
        {
            isAttackingCombo1 = true;
            charAnim.Play(attack_1_3Hash);
        }
    }
    public void SecondComboAtk()
    {
        isAttackingCombo2 = true;
        maxCombo_Limit = 1.5f;

        if (combo_2Step == 2)
        {
            charAnim.Play(attack_2_2Hash);
        }
        if (combo_2Step == 3)
        {
            charAnim.Play(attack_2_3Hash);
        }
        if (combo_2Step == 4)
        {
            charAnim.Play(attack_2_4Hash);
        }
    }
    void FirstCombo()
    {
        if (combo_1Step == 0)
        {
            isAttackingCombo1 = true;
            charAnim.Play(attack_1_1Hash);
            combo_1Step = 1;

            return;
        }
        if (combo_1Step != 0)
        {
            if (combo_1Possible)
            {
                combo_1Possible = false;
                combo_1Step += 1;
            }
        }
    }
    void SecondCombo()
    {
        if (combo_2Step == 0)
        {
            isAttackingCombo2 = true;
            charAnim.Play(attack_2_1Hash);
            combo_2Step = 1;
            return;
        }
        if (combo_2Step != 0)
        {
            if (combo_2Possible)
            {
                combo_2Possible = false;
                combo_2Step += 1;
            }
        }
    }
    void CloseGaurd()
    {
        charAnim.ResetTrigger("canStopGaurd");
    }
    void DisableCharController()
    {
        charController.enabled = false;
    }
    void EnableCharController()
    {
        charController.enabled = true;
    }
    void PlayMovementSound()                                        //below this functions are being called using Animation Events for Sounds
    {
        charWalk.Play();
    }
    void PlayRunSound()
    {
        charRun.Play();
    }
    void Play1_1AttackSound()
    {
        attackSound.PlayOneShot(attackClips[0]);
        swordSound.PlayOneShot(swordClips[0]);
    }
    void Play1_2AttackSound()
    {
        attackSound.PlayOneShot(attackClips[1]);
        swordSound.PlayOneShot(swordClips[1]);
    }

    void Play1_3AttackSound()
    {
        attackSound.PlayOneShot(attackClips[2]);
        swordSound.PlayOneShot(swordClips[2]);
    }

    void Play2_1AttackSound()
    {
        attackSound.PlayOneShot(attackClips[3]);
        swordSound.PlayOneShot(swordClips[3]);
    }
    void Play2_2AttackSound()
    {
        attackSound.PlayOneShot(attackClips[4]);
        swordSound.PlayOneShot(swordClips[4]);
    }
    void Play2_3AttackSound()
    {
        swordSound.PlayOneShot(swordClips[5]);
    }
    void Play2_4AttackSound()
    {
        attackSound.PlayOneShot(attackClips[5]);
        swordSound.PlayOneShot(swordClips[6]);
    }
    void PlayHurtSound()
    {
        attackSound.PlayOneShot(attackClips[7]);
    }
}
