using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public int addValue;
    public GameObject takeParticles;
    public float destroyTime;

    private SunManager sunManager;
    private Animator anim;

    private void Start()
    {
        sunManager = FindAnyObjectByType<SunManager>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {

        if(destroyTime <= 3)
            anim.SetTrigger("Destroy");

        if (destroyTime <= 0)
            Destroy(gameObject);

        destroyTime -= Time.deltaTime;


    }
    private void OnMouseDown()
    {
        if (sunManager != null)
        {
            Instantiate(takeParticles, transform.position, Quaternion.identity);
            sunManager.SunPlus(addValue);
            Destroy(gameObject);
        }
    }
}
