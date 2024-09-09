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
            if (!mainmenu && _GM.PlayerMovement == true)
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
            options.SetActive(true);
            _GM.pause();
            FindObjectOfType<gamemaster>().ShowCursor();
        }
        public void closeall2()
        {
            optionOn = false;
            options.SetActive(false);
            _GM.resume();
            FindObjectOfType<gamemaster>().HideCursor();
            FindObjectOfType<savedata>().SAVEGAME();
        }

        public void unpause() 
        {
            _GM.resume();
            FindObjectOfType<savedata>().SAVEGAME();
        }
    }
}
