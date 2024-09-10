using Global.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement : MonoBehaviour
{
    [Header("movement setting")]
    public CharacterController2D controller;
    public float normalrun;
    public float horizontalmove;

    [Header("condition_thing")]
    public bool onEvent;
    private bool jump;

    public float JumpStarted;
    public float JumpAdder;
    public float JumpForceSend;
    public float JumpMax;


    [Header("other component")]
    public Animator characterLeg;
    public Animator characterUpper;
    public Animator PlayerColor;
    private bool OnRespawn;
    public Rigidbody2D rigid;
    public Transform LastCheckPoint;

    [Header("Ember component")]
    public GameObject EmberTorso;
    public GameObject EmberTossing;

    public UnityEvent GotHitEvent;
    public UnityEvent AfterHitEvent;

    public GameObject Shadow;

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
        Physics2D.IgnoreLayerCollision(9, 10);
        Physics2D.IgnoreLayerCollision(8, 10);
        Physics2D.IgnoreLayerCollision(7, 10);

        LastCheckPoint = GameObject.FindGameObjectWithTag("firstCP").GetComponent<Transform>();
    }

    void Update()
    {
        if (onEvent == false && OnRespawn == false)
        {
            if (controller.m_Grounded)
            {
                horizontalmove = Input.GetAxis("Horizontal") * normalrun;
                characterLeg.SetFloat("jalan", Mathf.Abs(horizontalmove));
                characterUpper.SetFloat("jalan", Mathf.Abs(horizontalmove));               
                if (controller.m_midair == false)
                {
                    if (Input.GetButtonDown("Jump"))
                    {
                        FindObjectOfType<AudioManager>().SetCurrentSoundFXClip("charge");
                    }
                    if (Input.GetButton("Jump"))
                    {
                        JumOnhold();  
                    }
                    if (Input.GetButtonUp("Jump"))
                    {
                        Jumping();
                        FindObjectOfType<AudioManager>().StopCurrentSoundFXClip("charge");
                        FindObjectOfType<AudioManager>().SetCurrentSoundFXClip("jumping");
                    }
                }
                else
                {
                    characterLeg.SetBool("nahanLompat", false);
                    characterUpper.SetBool("nahanLompat", false);
                    FindObjectOfType<AudioManager>().StopCurrentSoundFXClip("charge");
                }
                Shadow.SetActive(true);
            }
            else
            {
                characterLeg.SetBool("nahanLompat", false);
                characterUpper.SetBool("nahanLompat", false);
                Shadow.SetActive(false);
            }
        }
        else
        {
            horizontalmove = 0;
            characterLeg.SetFloat("jalan", 0);
            characterUpper.SetFloat("jalan", 0);
            characterLeg.SetBool("nahanLompat", false);
            characterUpper.SetBool("nahanLompat", false);
            JumpForceSend = JumpStarted;
        }




        if (rigid.velocity.y <= -1)
        {
            characterLeg.SetBool("fall", true);
            characterUpper.SetBool("fall", true);
        }
        else
        {
            characterLeg.SetBool("fall", false);
            characterUpper.SetBool("fall", false);    
        }
        if (rigid.velocity.y >= 1)
        {
            characterLeg.SetBool("jump", true);
            characterUpper.SetBool("jump", true);
        }
        else
        {
            characterLeg.SetBool("jump", false);
            characterUpper.SetBool("jump", false);
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
        if (collision.tag == "Fall" && OnRespawn == false)
        {        
            StartCoroutine(Respawn());     
        }
    }

    public void Jumping()
    {
        jump = true;
        controller.UpdateJumpForce(JumpForceSend);
        characterLeg.SetBool("nahanLompat", false);
        characterUpper.SetBool("nahanLompat", false);
        JumpForceSend = JumpStarted;
    }

    public void JumOnhold()
    {
        horizontalmove = 0;
        if (JumpForceSend <= JumpMax)
        {
            JumpForceSend += JumpAdder;
        }
        characterLeg.SetBool("nahanLompat", true);
        characterUpper.SetBool("nahanLompat", true);
    }

    public IEnumerator Respawn()
    {
        FindObjectOfType<AudioManager>().SetCurrentSoundFXClip("dead");
        GotHitEvent.Invoke();
        PlayerColor.SetBool("lose", true);
        OnRespawn = true;
        var b = Instantiate(EmberTossing, EmberTorso.transform.position, EmberTorso.transform.rotation);
        Destroy(b.gameObject, 3);
        EmberTorso.SetActive(false);
        yield return new WaitForSeconds(1f);
        FindObjectOfType<AudioManager>().SetCurrentSoundFXClip("respawn");
        characterLeg.SetBool("nahanLompat", false);
        characterUpper.SetBool("nahanLompat", false);
        characterLeg.SetTrigger("respawn");
        characterUpper.SetTrigger("respawn");
        EmberTorso.SetActive(true);
        JumpForceSend = JumpStarted;     
        this.transform.position = LastCheckPoint.transform.position;
        PlayerColor.SetBool("lose", false);
        AfterHitEvent.Invoke();
        OnRespawn = false;
    }
}
