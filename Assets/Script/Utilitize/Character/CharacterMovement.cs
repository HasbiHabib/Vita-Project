using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("movement setting")]
    public CharacterController2D controller;
    public float normalrun;
    public float lari;
    public float horizontalmove;

    [Header("condition_thing")]
    public bool onEvent;
    private bool jump;

    public float JumpStarted;
    public float JumpAdder;
    public float JumpForceSend;
    public float JumpMax;


    [Header("other component")]
    //public Animator character;
    public Rigidbody2D rigid;
    public Transform LastCheckPoint;

    void Awake()
    {
        //on_cooldown = false;
        Physics2D.IgnoreLayerCollision(8, 0);
        Physics2D.IgnoreLayerCollision(3, 7);
        Physics2D.IgnoreLayerCollision(3, 8);
        Physics2D.IgnoreLayerCollision(7, 0);
        Physics2D.IgnoreLayerCollision(7, 6);
    }

    void Update()
    {
        if (onEvent == false)
        {
            if (controller.m_Grounded)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    horizontalmove = Input.GetAxis("Horizontal") * lari;
                    //character.SetFloat("jalan", Mathf.Abs(horizontalmove));
                }
                else
                {
                    horizontalmove = Input.GetAxis("Horizontal") * normalrun;
                    //character.SetFloat("jalan", Mathf.Abs(horizontalmove));
                }
                if (controller.m_midair == false)
                {
                    if (Input.GetButtonUp("Jump"))
                    {
                        jump = true; 
                        controller.UpdateJumpForce(JumpForceSend);
                        JumpForceSend = JumpStarted;
                    }
                    if (JumpForceSend <= JumpMax)
                    {
                        if (Input.GetButton("Jump"))
                        {
                            horizontalmove = 0;
                            JumpForceSend += JumpAdder;
                        }
                    }
                    else
                    {
                        jump = true;
                        controller.UpdateJumpForce(JumpForceSend);
                        JumpForceSend = JumpStarted;
                    }

                }
            }
        }
        else
        {
            horizontalmove = 0;
            //character.SetFloat("jalan", 0);
        }




        if (rigid.velocity.y <= -1)
        {
            //character.SetBool("fall", true);
        }
        else
        {
            //character.SetBool("fall", false);
        }
        if (rigid.velocity.y >= 1)
        {
            //character.SetBool("jump", true);
        }
        else
        {
            //character.SetBool("jump", false);
        }

        if (controller.m_midair == true)
        {
            if (Input.GetButtonDown("Jump"))
            {
                //slam = true;
            }
        }

    }

    void FixedUpdate()
    {
        controller.Move(horizontalmove * Time.fixedDeltaTime, jump, false);
        jump = false;
        //slam = false;
        //dash = false;
    }


    public void AfterEvent()
    {
        onEvent = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fall")
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        this.transform.position = LastCheckPoint.transform.position;
    }
}
