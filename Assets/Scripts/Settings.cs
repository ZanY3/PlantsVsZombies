using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public AudioClip buttonSound;

    public AudioMixer audioMixer;
    public GameObject panelCanvas;

    private AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void OpenSettingsPanel()
    {
        source.PlayOneShot(buttonSound);
        panelCanvas.SetActive(true);
    }
    public void CloseSettingsPanel()
    {
        source.PlayOneShot(buttonSound);
        panelCanvas.SetActive(false);
    }
}
