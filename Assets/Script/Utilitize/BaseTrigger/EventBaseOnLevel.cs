using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventBaseOnLevel : MonoBehaviour
{
    public int LevelSavedLoaded;
    public int SaveTriggerEvent;
    public UnityEvent EventTriggered;

    public GameObject TheTree;
    public TextMeshProUGUI ThePercentage;
    public string PercentageValue;
    public GameObject ButtonType;

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
        ButtonType.SetActive(true);
        ThePercentage.text = PercentageValue;
        TheTree.SetActive(true);
    }
}
