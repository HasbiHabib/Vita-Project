using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "WaterLiquids")
        {
            Destroy(collision.gameObject);
        }
    }
}
