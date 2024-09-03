using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneTransition : MonoBehaviour
{
	public Animator transisi;
	public float waktutransisi;
	public int addscene;
    public int toscene;
    public void opennextscene()
	{
        transisi = GameObject.FindGameObjectWithTag("transisi").GetComponent<Animator>();
        transisi.SetTrigger("out");
		StartCoroutine(waitasec());
	}


    public void openscene()
    {
        transisi = GameObject.FindGameObjectWithTag("transisi").GetComponent<Animator>();
        transisi.SetTrigger("out");
        StartCoroutine(waitasec2());
    }

    public void quit()
    {
    	Application.Quit();
    }

    IEnumerator waitasec()
    {
    	yield return new WaitForSecondsRealtime(waktutransisi);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + addscene);
    }
    IEnumerator waitasec2()
    {
        yield return new WaitForSecondsRealtime(waktutransisi);
        SceneManager.LoadScene(toscene);
    }
}
