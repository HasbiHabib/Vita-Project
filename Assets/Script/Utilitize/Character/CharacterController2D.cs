using Global.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D: MonoBehaviour
{
    [SerializeField] private float JumpForce;
    [SerializeField] private float m_Speedup = .36f;            // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [SerializeField] private float m_MovementSmoothing;   // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] public LayerMask m_WhatIsGround;                           // A mask determining what is ground to the character
    [SerializeField] public Transform m_GroundCheck;                            // A position marking where to check if the player is grounded.
    [SerializeField] public LayerMask m_WhatIsIce;

    public GameObject CharacterVisual;


    private bool firstland = true;
    public float IceSmooth;
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    public bool m_Grounded;            // Whether or not the player is grounded.
    public bool m_midair;            // Whether or not the player is midair.
    private Rigidbody2D m_Rigidbody;
    public bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;

    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;
    public UnityEvent OnJumpEvent;

    public BalancingTorso Balance;
    public CharacterMovement movement;

    private bool m_wasspeedup = false;

    public GameObject dust;

    public bool onDash = false;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        Balance = GetComponent<BalancingTorso>();
        movement = GetComponent<CharacterMovement>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void Update()
    {
        if (m_Grounded == true)
        {
            m_midair = false;
            if (firstland == true)
            {
                firstland = false;
            }
        }
        else
        {
            m_MovementSmoothing = 0;
        }
    }
    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                {
                    Instantiate(dust, m_GroundCheck.position, m_GroundCheck.rotation);
                    FindObjectOfType<AudioManager>().SetCurrentSoundFXClip("land");
                    Vector3 targetVelocity = new Vector2(0f, m_Rigidbody.velocity.y);
                    m_Rigidbody.velocity = Vector3.SmoothDamp(m_Rigidbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
                    OnLandEvent.Invoke();
                    CheckIced();
                }
                if (onDash == true)
                {
                    onDash = false;

                }
            }
        }

    }
    public void CheckIced()
    {
        Collider2D[] collider2s = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsIce);
        for (int b = 0; b < collider2s.Length; b++)
        {
            if (collider2s[b].gameObject != gameObject)
            {
                m_MovementSmoothing = IceSmooth;
            }
        }
    }

    public void UpdateJumpForce(float JumpSet)
    {
        JumpForce = JumpSet;
    }


    public void Move(float move, bool jump, bool speedup, bool Oncharge)
    {
        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            if (speedup)
            {
                if (!m_wasspeedup)
                {
                    m_wasspeedup = true;
                }
                move = m_Speedup;

            }
            else
            {
                if (m_wasspeedup)
                {
                    m_wasspeedup = false;
                }
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody.velocity = Vector3.SmoothDamp(m_Rigidbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody.AddForce(new Vector2(0f, JumpForce));
            OnJumpEvent.Invoke();
            //Instantiate(dust, m_GroundCheck.position, m_GroundCheck.rotation);
        }
        if (Oncharge)
        {
            m_MovementSmoothing = 0;
        }
    }



    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;
        // Multiply the player's x local scale by -1.
        Vector3 theScale = CharacterVisual.transform.localScale;
        theScale.x *= -1;
        CharacterVisual.transform.localScale = theScale;
    }
}
