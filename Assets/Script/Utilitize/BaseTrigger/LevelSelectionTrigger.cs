using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static AudioData;

public class LevelSelectionTrigger : MonoBehaviour
{
    public KeyCode KeyCodeInteract;

    public UnityEvent OnCollision;
    public UnityEvent OutCollision;
    public UnityEvent OnPressed;

    public bool CanInteract = false;

    public bool InsideTrigger;

    private void Update()
    {
        if (InsideTrigger)
        {
            if (Input.GetKeyDown(KeyCodeInteract))
            {
                Pressed();
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OnTriggerIn();
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OnTriggerOut();
        }
    }

    public void Pressed()
    {
        OnPressed.Invoke();
        OnTriggerOut();
        FindObjectOfType<gamemaster>().SetPlayerMove(false);
    }

    public void OnTriggerIn()
    {
        OnCollision.Invoke();
        InsideTrigger = true;
        CanInteract = true;
    }
    public void OnTriggerOut()
    {
        OutCollision.Invoke();
        InsideTrigger = false;
        CanInteract = false;
    }
}
