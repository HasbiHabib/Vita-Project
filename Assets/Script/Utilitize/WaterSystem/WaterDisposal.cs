using Global.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDisposal : MonoBehaviour
{
    public GameplayManager gameplayManager;

    private void Start()
    {
        gameplayManager = FindObjectOfType<GameplayManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "WaterLiquids")
        {
            gameplayManager.AddContent();
            Destroy(collision.gameObject,3);
        }
    }
}
