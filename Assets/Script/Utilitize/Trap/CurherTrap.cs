using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurherTrap : MonoBehaviour
{
    public float TimeOut;
    public float Timer;
    public Transform TrapOut;
    public GameObject TheBoulder;


    void Update()
    {
        if (Active)
        {
            if (Timer >= 0)
            {
                Timer -= Time.deltaTime;
            }
            else
            {
                InstantiateTrap();
            }
        }
    }


    public void InstantiateTrap()
    {
        Instantiate(TheBoulder, TrapOut.position, Quaternion.identity);
        Timer = TimeOut;
    }

    private bool Active = true;
    public void Deactive()
    {
        Active = false;
    }
}
