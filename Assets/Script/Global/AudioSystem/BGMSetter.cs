using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global.Audio;

public class BGMSetter : MonoBehaviour
{
    public string BGMName;
    public bool SetOnAwake = false;
    private bool SetTransition = false;
    float cooldown = 0.1f;

    private float Timeds;

    private void Update()
    {
        if (SetOnAwake)
        {
            cooldown -= Time.deltaTime;

            if (cooldown <= 0)
            {
                QuickChange(1);
                SetOnAwake = false;
            }
        }

        if (SetTransition) 
        {
            cooldown -= Time.deltaTime;

            if (cooldown <= 0)
            {
                FindObjectOfType<AudioManager>().QuickBGMChanger(BGMName, Timeds);
                SetTransition = false;
            }
        }
    }
    public void SetMusic()
    {
        FindObjectOfType<AudioManager>().SetCurrentBgmClip(BGMName);
    }

    public void SetMusicOn()
    {
        SetOnAwake = true;
        cooldown = 0.1f;
    }

    public void SetMusicOff() 
    {
        FindObjectOfType<AudioManager>().BGMStopper();
    }

    public void QuickChange(float Timed) 
    {
        Timeds = Timed;
        SetTransition = true;
        cooldown = 0.1f;
    }

    public void SetMusicHalf()
    {
        FindObjectOfType<AudioManager>().BGMSetHalf();
    }

    public void OpeningTransition(float Timed) 
    {
        FindObjectOfType<AudioManager>().OpeningTransition(Timed);
    }
}
