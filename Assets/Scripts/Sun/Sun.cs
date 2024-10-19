using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public int addValue;
    public GameObject takeParticles;
    public float destroyTime;
    public AudioClip sunTakeSound;

    private SunManager sunManager;
    private AudioSource source;
    private Animator anim;

    private void Start()
    {
        source = GameObject.FindGameObjectWithTag("SunSource").GetComponent<AudioSource>();
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
            source.PlayOneShot(sunTakeSound);
            Instantiate(takeParticles, transform.position, Quaternion.identity);
            sunManager.SunPlus(addValue);
            Destroy(gameObject);
        }
    }
}
