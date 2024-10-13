using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunflower : MonoBehaviour
{
    public GameObject sunPrefab;
    public GameObject spawnParticles;

    [Space]
    public float sunSpawnCD;
    public float minSpawnCD;
    public float maxSpawnCD;

    [Space]
    public SpriteRenderer headSpRndr;
    public Sprite defaultSprite;
    public Sprite spawnSprite;


    private Animator anim;
    private float minX, minY, maxX, maxY;
    private float startSunSpawnCD;

    private void Start()
    {
        minX = transform.position.x - 0.5f;
        maxX = transform.position.x + 0.5f;
        minY = transform.position.y - 0.5f;
        maxY = transform.position.y + 0.5f;

        sunSpawnCD = Random.Range(minSpawnCD, maxSpawnCD);
        startSunSpawnCD = sunSpawnCD;

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        sunSpawnCD -= Time.deltaTime;
        if(sunSpawnCD <= 2)
        {
            anim.SetBool("Spawn", true);
            headSpRndr.sprite = spawnSprite;
        }

        if(sunSpawnCD <= 0)
        {
            SpawnSun();
            sunSpawnCD = Random.Range(minSpawnCD, maxSpawnCD);
            startSunSpawnCD = sunSpawnCD;
        }
    }
    public void SpawnSun()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Vector2 spawnPosition = new Vector2(randomX, randomY);

        Instantiate(sunPrefab, spawnPosition, Quaternion.identity);
        Instantiate(spawnParticles, transform.position, Quaternion.identity);

        headSpRndr.sprite = defaultSprite;
        anim.SetBool("Spawn", false);
    }

}
