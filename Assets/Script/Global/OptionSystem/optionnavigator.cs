using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Option
{
    public class optionnavigator : MonoBehaviour
    {
        public GameObject options;

        public gamemaster _GM;

        private bool optionOn;
        public bool mainmenu;

        private void Start()
        {
            _GM = FindObjectOfType<gamemaster>();
        }

        void Update()
        {
            if (!mainmenu)
            {
                if (Input.GetButtonDown("esc"))
                {
                    if (optionOn == true)
                    {
                        closeall2();
                    }
                    else
                    {
                        openall2();
                    }
                }
            }
        }

        public void openall2()
        {
            StartCoroutine(tunggu3());
            options.SetActive(true);
            _GM.pause();
        }
        public void closeall2()
        {
            StartCoroutine(tunggu4());
            options.SetActive(false);
            _GM.resume();
            FindObjectOfType<savedata>().Saveoptions();
        }

        public void unpause() 
        {
            _GM.resume();
            FindObjectOfType<savedata>().Saveoptions();
        }

        IEnumerator tunggu3()
        {
            yield return new WaitForSecondsRealtime(0.3f);
            optionOn = true;
        }
        IEnumerator tunggu4()
        {
            yield return new WaitForSecondsRealtime(0.3f);
            optionOn = false;
        }
    }
}
