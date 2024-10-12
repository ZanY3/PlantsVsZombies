using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFromSkySpawner : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject sunPrefab;
    public float spawnCD;

    private float startSpawnCd;

    private void Start()
    {
        startSpawnCd = spawnCD;
    }
    private void Update()
    {
        spawnCD -= Time.deltaTime;
        if(spawnCD <= 0)
        {
            int randSpawner = Random.Range(0, spawners.Length);
            Instantiate(sunPrefab, spawners[randSpawner].transform.position, Quaternion.identity);
            spawnCD = startSpawnCd;
        }
    }
}
