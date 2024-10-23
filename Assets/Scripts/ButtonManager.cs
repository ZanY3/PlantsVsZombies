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
        Time.timeScale = 1f;
        source.PlayOneShot(buttonSound);
        SceneManager.LoadScene("Level 1");
    }
    public void LoadScene(string name)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(name);
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
