using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartedManager : MonoBehaviour
{
    public bool StartingApp = true;

    public void SetOpening(bool opening)
    {
        StartingApp = opening;
    }

    public bool GetOnGameState()
    {
        return StartingApp;
    }
}
