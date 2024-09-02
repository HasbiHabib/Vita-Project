using Global.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXtoChar : MonoBehaviour
{
    public string TheSFX;
    public void PlayTheDefaultSFX1()
    {
        FindObjectOfType<AudioManager>().SetCurrentSoundFXClip(TheSFX);
    }

    public string TheSFX2;
    public void PlayTheDefaultSFX2()
    {
        FindObjectOfType<AudioManager>().SetCurrentSoundFXClip(TheSFX2);
    }
    public string TheSFX3;
    public void PlayTheDefaultSFX3()
    {
        FindObjectOfType<AudioManager>().SetCurrentSoundFXClip(TheSFX3);
    }
}
