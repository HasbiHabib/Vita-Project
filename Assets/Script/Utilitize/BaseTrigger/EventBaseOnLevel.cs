using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventBaseOnLevel : MonoBehaviour
{
    public int LevelSavedLoaded;
    public int SaveTriggerEvent;
    public UnityEvent EventTriggered;
    public void Awake()
    {
        LevelSavedLoaded = FindObjectOfType<savedata>().LevelSaved;
        if (LevelSavedLoaded == SaveTriggerEvent)
        {
            StartEvent();
        }
    }

    public void StartEvent()
    {
        EventTriggered.Invoke();
    }
}
