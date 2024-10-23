using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float speed;
    public int health;
    public int damage;
    public float damageCD;

    public GameObject head;

    [Space]
    public bool isWithCone = false;
    public GameObject cone;


    [Space]
    public AudioClip damageSound;
    public AudioClip takeDamageSound;
    public AudioClip deathSound;

    private Rigidbody2D rb;
    private Animator anim;
    private float startDamageCD;
    private AudioSource source;
    private float randomPitch;
    private bool isPlayedDeathSound = false;
    private ZombieSpawner spawner;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startDamageCD = damageCD;
        source = GetComponent<AudioSource>();
        spawner = FindAnyObjectByType<ZombieSpawner>();

    }
    private void Update()
    {
        if(isWithCone && health <= health - 100)
        {
            cone.SetActive(false);
        }

        rb.velocity = new Vector2(speed, rb.velocity.y);

        if(health <= 0)
        {
            Death();
        }
        damageCD -= Time.deltaTime;
    }
    public async void Death()
    {
        //effects
        if(!isPlayedDeathSound)
        {
            isPlayedDeathSound = true;
            source.PlayOneShot(deathSound);
            spawner.enemiesLeft--;
        }

        head.SetActive(false);
        anim.SetTrigger("Death");
        await new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
    public void TakeDamage(int damage)
    {
        randomPitch = Random.Range(0.9f, 1.35f);
        source.pitch = randomPitch;
        source.PlayOneShot(takeDamageSound);

        anim.SetTrigger("TakeDamage");
        health -= damage;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plant"))
        {
            if (damageCD <= 0)
            {
                randomPitch = Random.Range(0.9f, 1.35f);
                source.pitch = randomPitch;

                source.PlayOneShot(damageSound);
                anim.SetTrigger("Damage");
                PlantHp plantHp = collision.gameObject.GetComponent<PlantHp>();
                plantHp.HealthMinus(damage);

                if (plantHp.health <= 0)
                {
                    Destroy(collision.gameObject);  // Это вызовет OnDestroy() у растения
                }

                damageCD = startDamageCD;
            }
        }
    }
}
