using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    public bool SetOnAwake = true;
    float cooldown = 0.1f;

    private void Update()
    {
        if (SetOnAwake)
        {
            cooldown -= Time.deltaTime;

            if (cooldown <= 0)
            {
                LevelSavedLoaded = FindObjectOfType<savedata>().LevelSaved;
                if (LevelSavedLoaded == SaveTriggerEvent)
                {
                    StartEvent();
                }
                SetOnAwake = false;
            }
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
