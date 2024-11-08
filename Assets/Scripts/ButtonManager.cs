using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public AudioClip buttonSound;

    private AudioSource source;

    private void Start()
    {
       source = GetComponent<AudioSource>();
    }

    public void Play()
    {
        source.PlayOneShot(buttonSound);
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1f;
    }
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1f;
    }    
    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }    
    public void Exit()
    {
        source.PlayOneShot(buttonSound);
        Application.Quit();
    }
}
