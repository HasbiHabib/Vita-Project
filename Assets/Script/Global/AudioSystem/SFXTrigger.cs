using Global.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXTrigger : MonoBehaviour
{
    public string TheSFX;

    public void PlayTheDefaultSFX() 
    {
        FindObjectOfType<AudioManager>().SetCurrentSoundFXClip(TheSFX);
    }

    public void PlayTheSFX(string clip)
    {
        FindObjectOfType<AudioManager>().SetCurrentSoundFXClip(clip);
    }
}
