using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpeningScreenBase : MonoBehaviour
{
    public UnityEvent SetupEvent;

    public UnityEvent MainMenuFirstEvent;
    public UnityEvent AlreadyOpenEvent;

    public Animator BlackScreen;

    private void Start()
    {
        SetupEvent.Invoke();

        if (FindObjectOfType<GameStartedManager>().GetOnGameState() == true)
        {
            MainMenuFirst();
            FindObjectOfType<GameStartedManager>().SetOpening(false);
            return;
        }
        else
        {
            AlreadyOpen();
            return;
        }
    }

    public void MainMenuFirst()
    {
        MainMenuFirstEvent.Invoke();
        BlackScreen.SetTrigger("out");
    }

    public void AlreadyOpen()
    {
        AlreadyOpenEvent.Invoke();
        BlackScreen.SetTrigger("out");
    }
}
