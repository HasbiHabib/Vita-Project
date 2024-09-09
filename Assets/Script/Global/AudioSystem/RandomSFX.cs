using Global.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSFX : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip[] TheClip;

    public void StartRandomSFX()
    {
        var RandomNum = Random.Range(0, TheClip.Length);
        AudioSource.clip = TheClip[RandomNum];
        AudioSource.Play();
    }
}
