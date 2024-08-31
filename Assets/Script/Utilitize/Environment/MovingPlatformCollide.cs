using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformCollide : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(this.transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
