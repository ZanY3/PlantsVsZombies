using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject panelCanvas;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void OpenSettingsPanel()
    {
        panelCanvas.SetActive(true);
    }
    public void CloseSettingsPanel()
    {
        panelCanvas.SetActive(false);
    }
}
