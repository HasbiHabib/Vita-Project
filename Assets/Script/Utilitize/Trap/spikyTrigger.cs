using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikyTrigger : MonoBehaviour
{
    public float OnTime;
    public float Timer;
    public float DelayTime;

    public Animator TheSpike;

    public Transform titikPointer;
    public float Range;
    public LayerMask InteractWith;

    private void Update()
    {
        if (Active)
        {
            if (Timer >= 0)
            {
                Timer -= Time.deltaTime;
            }
            else if (Timer <= 0)
            {

                if (Physics2D.Raycast(titikPointer.position, titikPointer.forward, Range, InteractWith))
                {
                    if (Timer <= 0)
                    {
                        StartCoroutine(SetOn());
                    }
                    Debug.DrawRay(titikPointer.position, titikPointer.forward * Range, Color.green);
                }
                else
                {
                    Debug.DrawRay(titikPointer.position, titikPointer.forward * Range, Color.blue);
                }
            }
        }
    }

    IEnumerator SetOn()
    {
        Timer = OnTime;
        yield return new WaitForSeconds(DelayTime);
        TheSpike.SetTrigger("Activated");
    }


    private bool Active = true;
    public void Deactive()
    {
        Active = false;
    }
}
