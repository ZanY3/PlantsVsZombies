using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeanutShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPos;
    public float shootCD;

    [Space]
    public float zombieSeeDistance = 10f;
    public LayerMask zombieLayerMask;

    private bool seeZombie = false;
    private float startShootCd;
    private Animator anim;

    private void Start()
    {
        startShootCd = shootCD;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {

        CheckForZombies();
        if (shootCD <= 0 && seeZombie)
        {
            Shoot();
            shootCD = startShootCd;
        }
        shootCD -= Time.deltaTime;
    }
    void CheckForZombies()
    {
        Vector2 direction = Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, zombieSeeDistance, zombieLayerMask);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Zombie"))
            {
                seeZombie = true;
            }
        }
        else
            seeZombie = false;

        Debug.DrawRay(transform.position, direction * zombieSeeDistance, Color.red);
    }
    public void Shoot()
    {
        anim.SetTrigger("Shoot");
        Instantiate(bulletPrefab, shootPos.position, Quaternion.identity);
    }

}
