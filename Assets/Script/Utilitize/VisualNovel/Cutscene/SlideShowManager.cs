using Global.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class SlideShowManager : MonoBehaviour
{
    public Image ImageShown;
    public Text ImageText;                           // text dialogue yang di display

    // float script
    public float textspeed;                           // kecepatan mengisi text

    // script ekstender / scdript pihak kedua

    private Queue<string> sentences;                  // text yang dibicarakan NPC
    private Queue<Sprite> ImageSprite;
    private string sentence;

    public SlideShowTrigger SlideShowTrigger;
    public gamemaster _GM;
    public GameObject TheSlideShowBar;

    public KeyCode nextKeyButton;
    public KeyCode nextKeyButton2;
    public bool TextUp;

    public bool OnSlideShow;

    public Animator TheBarAnimation;
    public float TransitionTime;

    public bool OnTransition;

    private float TimeToDisplayNext;

    void Start()
    {
        sentences = new Queue<string>();
        ImageSprite = new Queue<Sprite>();

        _GM = FindObjectOfType<gamemaster>();
    }
    public void StartDialogue(SlideShowTrigger dialog)
    {
        FindObjectOfType<AudioManager>().SetCurrentSoundFXClip("cardSwipe");
        SlideShowTrigger = dialog;
        TheSlideShowBar.SetActive(true);
        _GM.SetPlayerMove(false);
        OnSlideShow = true;
        sentences.Clear();
        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }
        ImageSprite.Clear();
        foreach (Sprite imagess in dialog.ImageShow)
        {
            ImageSprite.Enqueue(imagess);
        }

        TransitionBetween(TransitionTime/2, "start");
        
    }
    public void TransitionBetween(float timer, string animation) 
    {
        if (sentences.Count == 0)
        {
            //TheBarAnimation.Play("End");
            StartCoroutine(EndTheSlideShow());      
            return;
        }
        else 
        {
            //TheBarAnimation.Play(animation);
            StartCoroutine(TheTransitionBetween(timer));
            OnTransition = true;
        }
        
    }
    IEnumerator TheTransitionBetween(float timer) 
    {
        yield return new WaitForSeconds(timer/2);
        DisplayNextSentence();
        yield return new WaitForSeconds(timer/2);
        OnTransition = false;
    }
    public void DisplayNextSentence()
    {
        //FindObjectOfType<AudioManager>().SetCurrentSoundFXClip("cardSwipe");
        TimeToDisplayNext = 0;
        sentence = sentences.Dequeue();
        ImageText.text = sentence;
        Sprite wajahi = ImageSprite.Dequeue();
        ImageShown.sprite = wajahi;
        //string sounder = sound.Dequeue();
        //FindObjectOfType<soundmanager>().Play(sounder);
        StartCoroutine(Typesentence(sentence));
    }
    IEnumerator Typesentence(string sentence)
    {
        // agar text diisi perlahan
        ImageText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            ImageText.text += letter;
            yield return new WaitForSecondsRealtime(textspeed);
        }
        TextUp = true;
    }
    public void Fullsentence()
    {
        StopAllCoroutines();
        ImageText.text = sentence;
        TextUp = true;
    }
    IEnumerator EndTheSlideShow() 
    {
        OnTransition = true;
        yield return new WaitForSeconds(TransitionTime);
        OnTransition = false;
        EndDialogue();
    }
    void EndDialogue()
    {
        // dialogue berakhir
        sentences.Clear();
        ImageSprite.Clear();
        ImageText.text = "";
        ImageShown.sprite = null;
        TimeToDisplayNext = 0;

        TheSlideShowBar.SetActive(false);
        _GM.SetPlayerMove(true);
        OnSlideShow = false;
        SlideShowTrigger.FinishedStory.Invoke();
        SlideShowTrigger = null;
    }

    void Update()
    {
        if (OnSlideShow == true && !OnTransition && SlideShowTrigger.CanSkipped)
        {
            if (Input.GetKeyDown(nextKeyButton) && !TextUp || Input.GetKeyDown(nextKeyButton2) && !TextUp)
            {
                Fullsentence();
                return;
            }
            if (Input.GetKeyDown(nextKeyButton) && TextUp || Input.GetKeyDown(nextKeyButton2) && TextUp)
            {
                TextUp = false;
                TransitionBetween(TransitionTime, "next");
                return;
            }
        }

        if (TextUp && TimeToDisplayNext <= SlideShowTrigger.AutoDisplayNext)
        {
            TimeToDisplayNext += Time.deltaTime;
        }
        else if(TextUp && TimeToDisplayNext >= SlideShowTrigger.AutoDisplayNext) 
        {
            TransitionBetween(TransitionTime, "next");
            TextUp = false;
            return;
        }
    }
}
