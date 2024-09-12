using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    public VideoPlayer ThePlayer;
    public GameObject TheBar;
    public CutsceneTrigger TheTrigered;
    public UnityEvent EndEvent;
    public void PlayTheCutscene(VideoClip TheClip, CutsceneTrigger TheTrigger) 
    {
        TheBar.SetActive(true);
        TheTrigered = TheTrigger;
        ThePlayer.clip = TheClip;
        ThePlayer.Play();
        ThePlayer.loopPointReached += CheckOver;

    }

    void CheckOver(VideoPlayer vp)
    {
        ThePlayer.Stop();
        TheTrigered.FinishVideo.Invoke();
        TheTrigered = null;
        EndEvent.Invoke();
        TheBar.SetActive(false);
    }
}
