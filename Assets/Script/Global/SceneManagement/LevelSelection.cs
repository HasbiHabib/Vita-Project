using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    public int LevelSavedLoaded;

    public GameObject[] UnlockedLevel;

    public void Awake()
    {
        LevelSavedLoaded = FindObjectOfType<savedata>().LevelSaved;

        for (int i = 0; i < UnlockedLevel.Length; i++)
        {
            if(i <= LevelSavedLoaded)
            {
                UnlockedLevel[i].SetActive(true);
            }
        }
    }

}
