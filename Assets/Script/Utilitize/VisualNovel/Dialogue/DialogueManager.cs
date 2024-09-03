using Global.Audio;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header ("MainDialogueTarget")]
    public GameObject thedialogbar;
    public Animator DialogBar1Anim;
    public Text dialogtext;                           // text dialogue yang di display
    public Image CharacterSprite;                            // image dialogue yang di display
    public Text CharacterName;
    public Sprite empty;

    // float script
    public float textspeed;                           // kecepatan mengisi text

    // script ekstender / scdript pihak kedua

    private Queue<string> sentences;                  // text yang dibicarakan NPC
    private Queue<Sprite> SpriteChar;
    private Queue<string> NameChar;

    private string sentence;

    public DialogueTrigger dialogueTrigger;

    public gamemaster _GM;
    
    public bool ondialogue;

    public KeyCode nextKeyButton;
    public KeyCode nextKeyButton2;
    public bool TextUp;

    //public Animator thedialogbars;

    void Start()
    {
        // menentukan text si NPC
        sentences = new Queue<string>();
        SpriteChar = new Queue<Sprite>();
        NameChar = new Queue<string>();

        _GM = FindObjectOfType<gamemaster>();
    }

    // ngisi value dari DialogueTrigger
    public void StartDialogue(DialogueTrigger dialog)
    {
        // set the dialogue that suit the description
        dialogueTrigger = dialog;
        thedialogbar.SetActive(true);
        
        _GM.SetPlayerMove(false);
        ondialogue = true;

        // clear sentence...................
        sentences.Clear();
        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }
        // clear SpriteChar ...............
        SpriteChar.Clear();
        foreach (Sprite wajahi in dialog.wajah)
        {
            SpriteChar.Enqueue(wajahi);
        }
        // clear NameChar ...............
        NameChar.Clear();
        foreach (string namas in dialog.nama)
        {
            NameChar.Enqueue(namas);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //thedialogbars.SetTrigger("pop");   
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        sentence = sentences.Dequeue();

        FindObjectOfType<AudioManager>().SetCurrentSoundFXClip("buttonNext");
        dialogtext.text = sentence;
        // Set the Character Sprite, Name, and Sound
        Sprite wajahi = SpriteChar.Dequeue();
        CharacterSprite.sprite = wajahi;
        string namas = NameChar.Dequeue();
        CharacterName.text = namas;
        //DialogBar1Anim.SetTrigger("next");

        StopAllCoroutines();
        StartCoroutine(Typesentence(sentence));
    }
    IEnumerator Typesentence(string sentence)
    {
        // agar text diisi perlahan
        TextUp = false;
        dialogtext.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogtext.text += letter;
            yield return new WaitForSecondsRealtime(textspeed);
        }
        TextUp = true;
    }
    public void Fullsentence() 
    {
        StopAllCoroutines();
        dialogtext.text = sentence;
        TextUp = true;
    }

    void EndDialogue()
    {
        // dialogue berakhir
        NameChar.Clear();
        sentences.Clear();
        SpriteChar.Clear();
        dialogtext.text = "";
        CharacterName.text = "";
        CharacterSprite.sprite = empty;
        TextUp = false;

        thedialogbar.SetActive(false);
        _GM.SetPlayerMove(true);
        ondialogue = false;
        dialogueTrigger.dialogselesai.Invoke();
    }

    void Update()
    {
        if (ondialogue == true)
        {
            if(Input.GetKeyDown(nextKeyButton) && !TextUp || Input.GetKeyDown(nextKeyButton2) && !TextUp)
            {
                Fullsentence();
                return;
            }
            if (Input.GetKeyDown(nextKeyButton) && TextUp || Input.GetKeyDown(nextKeyButton2) && TextUp)
            {
                TextUp = false;
                DisplayNextSentence();
                return;
            }
        }
    }
}
