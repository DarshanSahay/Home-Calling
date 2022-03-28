using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    [SerializeField] private Animator anim;
    [SerializeField] private Transform cam;

    private float speed;
    private float gravity;
    private Vector3 direction;

    public CharacterController controller;
    public bool isPlayerDead = false;

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        statehandler.activeState = PlayerStates.Move;
    }
    public override void OnStateExit()
    {
        base.OnStateExit();
    }
    private void Start()
    {
        gravity = -9.18f;
        anim = GetComponent<Animator>();
        statehandler = PlayerStateHandler.Instance;
    }
    private void Update()
    {
        if (!PlayerInputHandler.Instance.GetPlayerMoveInput())
        {
            statehandler.currentState.ChangeState(statehandler.idleState);
        }
        Movement();
        OnHoldSprint();
    }
    private void OnHoldSprint()                                       //changing speed on holding shift button
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 5;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 2;
        }
    }
    private void Movement()                                                   //function to control player movement
    {
        float horizonatal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        direction = new Vector3(horizonatal, 0f, vertical).normalized;
        direction = Quaternion.AngleAxis(cam.rotation.eulerAngles.y, Vector3.up) * direction;
        direction.Normalize();

        if (direction != Vector3.zero)
        {
            anim.SetBool("canMoveForward", true);
            Quaternion rot = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 180f * Time.deltaTime);
            anim.SetFloat("InputMagnitude", speed);
        }
        else
        {
            anim.SetBool("canMoveForward", false);
        }

        controller.Move(new Vector3(direction.x * Time.deltaTime, gravity * Time.deltaTime, direction.z * Time.deltaTime) * speed);
    }
}
