using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    public void ResetTheGame()
    {
        FindObjectOfType<GameStartedManager>().SetOpening(true);
        FindObjectOfType<savedata>().ResetLevel();
    }
}
