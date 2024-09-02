using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireTrap : MonoBehaviour
{
    public UnityEvent OnActiveEvent;
    public UnityEvent OnInactiveEvent;

    public float OffTime;
    public float OnTime;

    public float Timer;
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
        OnActiveEvent.Invoke();
        Timer = OnTime;
        OnActive = true;
    }
    public void SetOff()
    {
        OnInactiveEvent.Invoke();
        Timer = OffTime;
        OnActive = false;
    }


    private bool Active = true;
    public void Deactive()
    {
        SetOff();
        Active = false;        
    }
}
