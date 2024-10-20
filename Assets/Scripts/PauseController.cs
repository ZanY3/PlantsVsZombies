using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public GameObject pausePanel;
    public AudioClip pauseSound;


    private bool isPaused = false;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void Pause()
    {
        if (!isPaused)
        {
            source.PlayOneShot(pauseSound);
            pausePanel.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
        }

    }
    public void UnPause()
    {
        if (isPaused)
        {
            source.pitch = 2f;
            source.PlayOneShot(pauseSound);
            pausePanel.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
        }
    }    

}
