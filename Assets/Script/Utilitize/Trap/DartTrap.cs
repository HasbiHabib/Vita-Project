using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTrap : MonoBehaviour
{
    public Transform titikPointer;
    public float Range;
    public LayerMask InteractWith;

    public float TimeOut;
    public float Timer;
    public Transform TrapOut;
    public GameObject TheBoulder;

    public float BulletSpeed;

    private void Update()
    {
        if (Active)
        {
            if (Physics2D.Raycast(titikPointer.position, titikPointer.forward, Range, InteractWith))
            {
                if (Timer <= 0)
                {
                    InstantiateTrap();
                }
                Debug.DrawRay(titikPointer.position, titikPointer.forward * Range, Color.green);
            }
            else
            {
                Debug.DrawRay(titikPointer.position, titikPointer.forward * Range, Color.blue);
            }

            if (Timer >= 0)
            {
                Timer -= Time.deltaTime;
            }
        }
    }
    public void InstantiateTrap()
    {
        var b = Instantiate(TheBoulder, TrapOut.position, TrapOut.rotation);
        b.GetComponent<Rigidbody2D>().velocity = b.transform.TransformDirection(Vector3.left * BulletSpeed);
        Destroy(b.gameObject, 10);
        Timer = TimeOut;
    }

    private bool Active = true;
    public void Deactive()
    {
        Active = false;
    }
}
