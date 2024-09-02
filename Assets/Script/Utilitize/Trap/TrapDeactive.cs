using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrapDeactive : MonoBehaviour
{
    public UnityEvent DeactivatedTrap;

    public Transform titikPointer;
    public float Range;
    public LayerMask InteractWith;

    public bool Pressed = false;

    public void StartTheDeactivated()
    {
        DeactivatedTrap?.Invoke();
    }

    private void Update()
    {
        if (Pressed == false)
        {
            if (Physics2D.Raycast(titikPointer.position, titikPointer.forward, Range, InteractWith))
            {

                StartTheDeactivated();
                Pressed = true;
                Debug.DrawRay(titikPointer.position, titikPointer.forward * Range, Color.green);
            }
            else
            {
                Debug.DrawRay(titikPointer.position, titikPointer.forward * Range, Color.blue);
            }
        }
    }
        
}
