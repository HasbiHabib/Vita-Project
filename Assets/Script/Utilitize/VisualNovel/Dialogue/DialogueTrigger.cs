using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public string[] nama;
    public Sprite[] wajah;

    [TextArea(3, 10)]
    public string[] sentences;

    [Header("Events")]
    [Space]
    public UnityEvent dialogselesai;
    public UnityEvent dialogmulai;

    public void startTheDialogue()
    {
        StartCoroutine(mulaidialogue());
        dialogmulai.Invoke();
    }

    IEnumerator mulaidialogue()
    {
        yield return new WaitForSeconds(0.001f);
        FindObjectOfType<DialogueManager>().StartDialogue(this);
    }
}
