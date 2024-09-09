using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TreeGrowingTimeline : MonoBehaviour
{
    public bool StartWhenAwake;
    public UnityEvent TheEvent;  
    public float SequenceTime;
    public UnityEvent EventEnd;

    public void Start()
    {
        if (StartWhenAwake)
        {
            StartTheEvent();
        }
    }

    public void StartTheEvent()
    {
        StartCoroutine(EventActive());
        TheEvent.Invoke();
    }

    IEnumerator EventActive()
    {
        yield return new WaitForSeconds(SequenceTime);
        EventEnd.Invoke();
    }
}
