using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.Events;
using Global.Option;

[System.Serializable]
public class saveproggres
{
    public float sfx;
    public float BGM;
    public saveproggres(savedata player)
    {
        sfx = player.sfx;
        BGM = player.BGM;
    }
}

public class savedata : MonoBehaviour
{
    public bool canLoad;

    [Header("Option Setting")]
    public float sfx;
    public float BGM;

    void Start()
    {
        if (canLoad == true)
        {
            LoadOptions();
        }
        Saveoptions();
    }
    public void Saveoptionssetting(float SFX, float bgm)
    {
        sfx = SFX;
        BGM = bgm;
        SaveSystem.saveoption(this);
    }

    public void Saveoptions()
    {
        SaveSystem.saveoption(this);
    }
    public void LoadOptions()
    {
        saveproggres data2 = SaveSystem.Loadoption();
        sfx = data2.sfx;
        BGM = data2.BGM;
        FindObjectOfType<option>().LoadSetting(sfx, BGM);
    }
}
