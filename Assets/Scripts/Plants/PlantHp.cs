using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHp : MonoBehaviour
{
    public int health;
    public string plantCardManagerTag;

    private CardManager cardManager;
    private Animator anim;

    private void Start()
    {
        cardManager = GameObject.FindGameObjectWithTag(plantCardManagerTag).GetComponent<CardManager>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(health <= 0)
        {
            // death anim;
            cardManager.ExemptCell();
            Destroy(gameObject);
        }
    }
    public void HealthMinus(int count)
    {
        anim.SetTrigger("TakeDamage");
        health -= count;
    }
}
