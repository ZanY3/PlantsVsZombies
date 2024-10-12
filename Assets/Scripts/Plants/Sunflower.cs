using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunflower : MonoBehaviour
{
    public GameObject sunPrefab;
    public GameObject spawnParticles;
    public float sunSpawnCD;

    [Space]
    public Sprite defaultSprite;
    public Sprite spawnSprite;


    private float minX, minY, maxX, maxY;
    private float startSunSpawnCD;

    private void Start()
    {
        minX = transform.position.x - 0.5f;
        maxX = transform.position.x + 0.5f;
        minY = transform.position.y - 0.5f;
        maxY = transform.position.y + 0.5f;

        startSunSpawnCD = sunSpawnCD;
    }

    private void Update()
    {
        sunSpawnCD -= Time.deltaTime;
        if(sunSpawnCD <= 3)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spawnSprite;
        }

        if(sunSpawnCD <= 0)
        {
            SpawnSun();
            sunSpawnCD = startSunSpawnCD;
        }
    }
    public void SpawnSun()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Vector2 spawnPosition = new Vector2(randomX, randomY);

        Instantiate(sunPrefab, spawnPosition, Quaternion.identity);
        Instantiate(spawnParticles, transform.position, Quaternion.identity);

        gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
    }

}
