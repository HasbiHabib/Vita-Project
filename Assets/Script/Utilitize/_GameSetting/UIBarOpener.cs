using Global.Audio;
using Global.Option;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIBarOpener : MonoBehaviour
{
    public GameObject Bar;
    public gamemaster _GM;
    public Animator TheAnimationBar;
    public UnityEvent CloseBarEvent;
    public UnityEvent OpenBarEvent;

    private void Start()
    {
        _GM = FindObjectOfType<gamemaster>();
    }

    public void OpenBar()
    {
        Bar.SetActive(true);
        TheAnimationBar.SetTrigger("in");         
        OpenBarEvent.Invoke();
        _GM.ShowCursor();
        _GM.SetPlayerMove(false);
        FindObjectOfType<gamemaster>().ShowCursor();
        FindObjectOfType<AudioManager>().SetCurrentSoundFXClip("komputerOpen");
    }
    public void CloseBar()
    {         
        TheAnimationBar.SetTrigger("out");
        FindObjectOfType<AudioManager>().SetCurrentSoundFXClip("komputerTouch1");
        StartCoroutine(DelayTheClosing());
    }

    IEnumerator DelayTheClosing()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Bar.SetActive(false);
        CloseBarEvent.Invoke();
        _GM.SetPlayerMove(true);
        _GM.HideCursor();
    }

    public void unpause()
    {
        _GM.resume();
        FindObjectOfType<option>().SaveOption();
    }
}
