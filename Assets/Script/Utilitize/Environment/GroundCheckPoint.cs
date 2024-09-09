using Global.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckPoint : MonoBehaviour
{
    public CharacterMovement player;
    public Transform CheckPoint;
    public GameObject WaterDrop;

    void Start()
    {
        player = FindObjectOfType<CharacterMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "WaterLiquids")
        {
            Instantiate(WaterDrop,collision.gameObject.transform.position,Quaternion.identity);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            player.LastCheckPoint = CheckPoint;
        }
    }

    

}
