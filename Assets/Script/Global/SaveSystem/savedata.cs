using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.Events;
using Global.Option;
using UnityEngine.SocialPlatforms;

[System.Serializable]
public class saveproggres
{
    public float sfx;
    public float BGM;

    public int LevelSaved;

    public saveproggres(savedata player)
    {
        sfx = player.sfx;
        BGM = player.BGM;
        LevelSaved = player.LevelSaved;
    }
}

public class savedata : MonoBehaviour
{
    public bool canLoad;

    [Header("Option Setting")]
    public float sfx;
    public float BGM;

    [Header("Save Data Player")]
    public int LevelSaved;


    void Start()
    {
        if (canLoad == true)
        {
            LOADGAME();
        }
        SAVEGAME();
    }
    public void Saveoptionssetting(float SFX, float bgm)
    {
        sfx = SFX;
        BGM = bgm;
        SaveSystem.saveoption(this);
    }


    public void ModifyLevelSaved(int UpdateTo)
    {
        if(LevelSaved <= UpdateTo)
        {
            LevelSaved = UpdateTo;
        }
        SAVEGAME();
    }

    public void ResetLevel()
    {
        LevelSaved = 0;
        SAVEGAME();
    }


    public void SAVEGAME()
    {
        SaveSystem.saveoption(this);
    }
    public void LOADGAME()
    {
        saveproggres data2 = SaveSystem.Loadoption();
        if (data2 == null)
        {
            FindObjectOfType<option>().NullLoad();
        }
        else
        {
            sfx = data2.sfx;
            BGM = data2.BGM;
            FindObjectOfType<option>().LoadSetting(sfx, BGM);
            LevelSaved = data2.LevelSaved;
        }
    }
}
