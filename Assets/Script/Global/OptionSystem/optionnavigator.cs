using Global.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Global.Option
{
    public class optionnavigator : MonoBehaviour
    {
        public GameObject options;

        public gamemaster _GM;

        private bool optionOn;
        public bool mainmenu;

        public bool CanTriigger = true;

        public Animator TheAnimationBar;

        public UnityEvent CloseEvent;

        private void Start()
        {
            _GM = FindObjectOfType<gamemaster>();
        }

        void Update()
        {
            if (!mainmenu && _GM.PlayerMovement == true && CanTriigger == true)
            {
                if (Input.GetButtonDown("esc"))
                {
                    if (optionOn == true)
                    {           
                        closeall2();
                        return;
                    }
                    else
                    {
                        openall2();
                        return;
                    }
                }
            }
        }

        public void openall2()
        {
            optionOn = true;
            CanTriigger = false;
            FindObjectOfType<gamemaster>().ShowCursor();    
            FindObjectOfType<AudioManager>().SetCurrentSoundFXClip("pausedbutton");
            StartCoroutine(DelayTheOpening());
        }
        public void closeall2()
        {
            CloseEvent.Invoke();
            optionOn = false;
            CanTriigger = false;
            FindObjectOfType<gamemaster>().HideCursor();
            FindObjectOfType<option>().SaveOption();
            FindObjectOfType<AudioManager>().SetCurrentSoundFXClip("button4");
            StartCoroutine(DelayTheClosing());
        }

        IEnumerator DelayTheOpening()
        {
            options.SetActive(true);
            TheAnimationBar.SetTrigger("in");
            _GM.pause();  
            yield return new WaitForSecondsRealtime(0.5f);
            CanTriigger = true;
        }

        IEnumerator DelayTheClosing()
        {       
            TheAnimationBar.SetTrigger("out");
            _GM.resume();       
            yield return new WaitForSecondsRealtime(0.5f);
            options.SetActive(false);
            CanTriigger = true;
        }

        public void unpause() 
        {
            _GM.resume();
            FindObjectOfType<option>().SaveOption();
        }
    }
}
