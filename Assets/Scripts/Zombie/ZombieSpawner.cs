using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public GameObject winPanel;

    public List<Transform> spawnPoints;

    [Header("Settings")]
    [Range(0.1f, 10f)] public float spawnInterval = 1f;
    public int enemiesLeft;
    public int enemiesToSpawn;
    public List<int> enemiesPerWave;

    [Space]
    public AudioClip waveStartSound;

    [Range(0.1f, 20f)] public float timeBetweenWaves;

    [Space]
    public AudioClip winSound;

    private int waveCount;
    private AudioSource source;
    private bool isUsedWin = false;

    void Spawn()
    {
        var point = spawnPoints[Random.Range(0, spawnPoints.Count)];
        Instantiate(zombiePrefab, point.position, Quaternion.identity);
    }
    async void Start()
    {
        source = GetComponent<AudioSource>();

        foreach (var num in enemiesPerWave) //waves
        {
            await new WaitForSeconds(timeBetweenWaves);
            enemiesToSpawn = num;
            OnWaveStart();
            waveCount++;
            await new WaitForSeconds(2f);
            while (enemiesToSpawn > 0)
            {
                await new WaitForSeconds(spawnInterval);
                Spawn();
                enemiesToSpawn--;
            }
        }

    }
    private async void Update()
    {   
        if(enemiesLeft <= 0 && !isUsedWin)
        {
            //onWavesCleared
            source.PlayOneShot(winSound);
            await new WaitForSeconds(2);
            winPanel.SetActive(true);
            await new WaitForSeconds(2);
            Time.timeScale = 0f;
            isUsedWin = true;
        }
    }

    public void OnWaveStart()
    {
        if(enemiesLeft > 0)
            source.PlayOneShot(waveStartSound);
    }
}
