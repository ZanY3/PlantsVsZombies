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

    private Rigidbody2D rb;
    private Animator anim;
    private float startDamageCD;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startDamageCD = damageCD;
    }
    private async void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);

        if(health <= 0)
        {
            //sounds, effects
            head.SetActive(false);
            anim.SetTrigger("Death");
            await new WaitForSeconds(1.5f);
            Destroy(gameObject);
        }
        damageCD -= Time.deltaTime;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Plant"))
        {
            if(damageCD <= 0)
            {
                anim.SetTrigger("Damage");
                collision.gameObject.GetComponent<PlantHp>().HealthMinus(damage);
                damageCD = startDamageCD;
            }
        }
    }
}
