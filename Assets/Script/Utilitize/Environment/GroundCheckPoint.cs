using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckPoint : MonoBehaviour
{
    public CharacterMovement player;
    public Transform CheckPoint;
    public float WaterLiquids = 2;

    void Start()
    {
        player = FindObjectOfType<CharacterMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "WaterLiquids")
        {
            Destroy(collision.gameObject, WaterLiquids);
        }

        if (collision.gameObject.tag == "Player")
        {
            player.LastCheckPoint = CheckPoint;
        }
    }

    

}
