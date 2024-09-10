using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialGoTo : MonoBehaviour
{
    public UnityEvent Trigger;
    public float DestroyTime;
    public Animator Animations;
    public GameObject Parent;

    private bool first = true;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && first)
        {
            Animations.SetTrigger("out");
            Trigger.Invoke();
            Destroy(Parent,DestroyTime);
            first = false;
        }
    }
}
