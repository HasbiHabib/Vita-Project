using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementBase : MonoBehaviour
{
    [Header("movement setting")]
    public CharacterController2D controller;
    public float normalrun;
    public float horizontalmove;
    public bool onEvent;


    [Header("other component")]
    public Animator CharacterAnim;
    public Rigidbody2D rigid;

    void Awake()
    {
        //on_cooldown = false;
        Physics2D.IgnoreLayerCollision(8, 0);
        Physics2D.IgnoreLayerCollision(3, 7);
        Physics2D.IgnoreLayerCollision(3, 8);
        Physics2D.IgnoreLayerCollision(7, 0);
        Physics2D.IgnoreLayerCollision(7, 6);
        Physics2D.IgnoreLayerCollision(9, 7);
        Physics2D.IgnoreLayerCollision(9, 8);
        Physics2D.IgnoreLayerCollision(9, 3);
        Physics2D.IgnoreLayerCollision(9, 9);
    }

    void Update()
    {
        if (onEvent == false)
        {
            if (controller.m_Grounded)
            {
                horizontalmove = Input.GetAxis("Horizontal") * normalrun;
                CharacterAnim.SetFloat("jalan", Mathf.Abs(horizontalmove));
            }
        }
        else
        {
            horizontalmove = 0;
        }

    }

    void FixedUpdate()
    {
        controller.Move(horizontalmove * Time.fixedDeltaTime, false, false);
        //slam = false;
        //dash = false;
    }


    public void AfterEvent()
    {
        onEvent = false;
    }
}
