using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemaster : MonoBehaviour
{
	public bool paused = false;
    public bool Canpause = false;
    
     public void resume()
     {
        Time.timeScale = 1;
        paused = false;
     }

     public void pause()
     {
        Time.timeScale = 0;
        paused = true;
     }

    public void SetCanPause(bool Fill)
    {
        Canpause = Fill;
    }

}
