using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class TutorialKeyIndicator : MonoBehaviour
{
    public GameObject Parent;
    public bool AutoDestroy;
    public float DestroyTime;
    public float FadeOutTime;
    public Animator AnimationForTutorial;

    public bool StartingDelay;
    public GameObject KeyBar;
    public float DelayTime;

    public void StartIn()
    {
        KeyBar.SetActive(true);
        AnimationForTutorial.SetTrigger("in");
    }

    private void Start()
    {
        if (AutoDestroy)
        {
            StartCoroutine(DestryoTimer());
        }

        if (StartingDelay)
        {
            StartCoroutine(StartDelaying());
        }
    }
    IEnumerator StartDelaying()
    {
        yield return new WaitForSeconds(DelayTime);
        StartIn();
    }

    IEnumerator DestryoTimer()
    {
        yield return new WaitForSeconds(DestroyTime);
        AnimationForTutorial.SetTrigger("out");
        Destroy(Parent.gameObject, FadeOutTime);
    }
}
