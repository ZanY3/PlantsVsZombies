using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public List<Transform> spawnPoints;

    [Header("Settings")]
    [Range(0.1f, 10f)] public float spawnInterval = 1f;
    public int enemiesToSpawn;
    public List<int> enemiesPerWave;

    [Space]
    public AudioClip waveStartSound;

    [Range(0.1f, 10f)] public float timeBetweenWaves;

    private int waveCount;
    private AudioSource source;

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
            //onWaveEnd
            await new WaitForSeconds(timeBetweenWaves);
        }
        //onWavesCleared

    }

    public void OnWaveStart()
    {
        source.PlayOneShot(waveStartSound);
    }
}
