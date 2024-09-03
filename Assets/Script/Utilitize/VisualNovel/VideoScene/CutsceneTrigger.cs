using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class CutsceneTrigger : MonoBehaviour
{
    public VideoClip TheClip;
    public UnityEvent FinishVideo;

    public void SetCutsceneStart() 
    {
        FindObjectOfType<CutsceneManager>().PlayTheCutscene(TheClip, this);
    }
}
