using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public UnityEvent WinCondition;
    public UnityEvent LoseCondition;
    public Slider Progression;

    public int MaxCapacity;
    public int WaterContent;

    private void Start()
    {
        Progression.maxValue = MaxCapacity;
        Progression.value = WaterContent;
    }

    public void AddContent()
    {
        if(WaterContent <= MaxCapacity)
        {
            WaterContent++;
            Progression.value = WaterContent;
        }
        if(WaterContent >= MaxCapacity)
        {
            WinCondition.Invoke();
            return;
        }
    }
}
