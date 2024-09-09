using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public int LevelSavedLoaded;
    public int SceneSkip;

    public void Awake()
    {
        LevelSavedLoaded = FindObjectOfType<savedata>().LevelSaved;
        toscene = LevelSavedLoaded + SceneSkip;
    }

    public Animator transisi;
    public float waktutransisi;
    public int toscene;

    public void StartMisi()
    {
        transisi = GameObject.FindGameObjectWithTag("transisi").GetComponent<Animator>();
        transisi.SetTrigger("out2");
        StartCoroutine(waitasec2());
    }
    IEnumerator waitasec2()
    {
        yield return new WaitForSecondsRealtime(waktutransisi);
        SceneManager.LoadScene(toscene);
    }
}
