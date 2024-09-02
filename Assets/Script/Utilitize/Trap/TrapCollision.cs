using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrapCollision : MonoBehaviour
{
    public bool DestroyOnImpact;
    public float DestroyDelay;
    public UnityEvent HitGround;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            HitGround.Invoke();

            if (DestroyOnImpact)
            {
                Destroy(this.gameObject, DestroyDelay);
            }
        }
    }
}
