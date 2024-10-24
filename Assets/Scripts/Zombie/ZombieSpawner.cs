using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class ZombieSpawner : MonoBehaviour
{
    public List<GameObject> zombiePrefabs;
    public List<float> spawnChances;

    [Space]
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
        GameObject selectedZombiePrefab = GetRandomZombiePrefab();

        if (selectedZombiePrefab != null)
        {
            Instantiate(selectedZombiePrefab, point.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Не удалось выбрать префаб зомби для спавна.");
        }
    }

    GameObject GetRandomZombiePrefab()
    {
        if (zombiePrefabs.Count == 0 || spawnChances.Count == 0 || zombiePrefabs.Count != spawnChances.Count)
        {
            Debug.LogError("Списки префабов и шансов должны быть одинакового размера и не пустыми.");
            return null;
        }

        float totalChance = spawnChances.Sum(); // Суммируем все шансы
        float randomValue = Random.Range(0f, totalChance); // Генерируем случайное число в диапазоне от 0 до суммы шансов

        float cumulativeChance = 0f;

        for (int i = 0; i < zombiePrefabs.Count; i++)
        {
            cumulativeChance += spawnChances[i];
            if (randomValue <= cumulativeChance)
            {
                return zombiePrefabs[i];
            }
        }

        return null;
    }

    async void Start()
    {
        source = GetComponent<AudioSource>();

        foreach (var num in enemiesPerWave) // Волны
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
        if (enemiesLeft <= 0 && !isUsedWin)
        {
            // Все волны зачищены
            isUsedWin = true;
            source.PlayOneShot(winSound);
            await new WaitForSeconds(2);
            winPanel.SetActive(true);
            await new WaitForSeconds(2);
            Time.timeScale = 0f;
        }
    }

    public void OnWaveStart()
    {
        if (enemiesLeft > 0)
            source.PlayOneShot(waveStartSound);
    }
}
