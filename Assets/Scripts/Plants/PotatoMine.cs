using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoMine : MonoBehaviour
{
    public Sprite readyToExplodeSprite;
    public GameObject explosionParticles;
    public float readyCd;

    private SpriteRenderer sRenderer;
    private bool isReady = false;

    private void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();

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
            collision.gameObject.GetComponent<ZombieController>().health = 0;
            Instantiate(explosionParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
