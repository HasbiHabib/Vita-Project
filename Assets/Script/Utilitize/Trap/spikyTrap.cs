using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikyTrap : MonoBehaviour
{
    public float OnTime;
    public float OffTime;
    public float Timer;

    public Animator TheSpike;

    public bool OnActive;

    private void Update()
    {
        if (Active)
        {
            if (Timer >= 0)
            {
                Timer -= Time.deltaTime;
            }
            else
            {
                if (OnActive)
                {
                    SetOff();
                    return;
                }
                else
                {
                    SetOn();
                    return;
                }
            }
        }
    }

    public void SetOn()
    {
        Timer = OnTime;
        OnActive = true;
        TheSpike.SetBool("Activated", true);
    }

    public void SetOff()
    {
        Timer = OffTime;
        OnActive = false;
        TheSpike.SetBool("Activated", false);
    }

    private bool Active = true;
    public void Deactive()
    {
        SetOff();
        Active = false;
    }
}
