using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlideShowTrigger : MonoBehaviour
{
    public bool CanSkipped;
    public float AutoDisplayNext;
    public Sprite[] ImageShow;
    [TextArea(3, 10)]
    public string[] sentences;

    [Header("Events")]
    [Space]
    public UnityEvent FinishedStory;

    public void StartTheSlideShow()
    {
        StartCoroutine(startSlide());
    }

    IEnumerator startSlide()
    {
        yield return new WaitForSeconds(0.001f);
        FindObjectOfType<SlideShowManager>().StartDialogue(this);
    }
}
