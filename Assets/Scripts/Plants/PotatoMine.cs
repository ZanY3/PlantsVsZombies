using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoMine : MonoBehaviour
{
    public Sprite readyToExplodeSprite;
    public GameObject explosionParticles;
    public float readyCd;

    [Space]
    public AudioClip explosionSound;

    private SpriteRenderer sRenderer;
    private bool isReady = false;
    private AudioSource source;

    private void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        source = GameObject.FindGameObjectWithTag("MineSource").GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (readyCd <= 0)
        {
            isReady = true;
            sRenderer.sprite = readyToExplodeSprite;
        }
        else
            readyCd -= Time.deltaTime;
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Zombie") && isReady)
        {
            source.PlayOneShot(explosionSound);

            collision.gameObject.GetComponent<ZombieController>().health = 0;
            Instantiate(explosionParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
