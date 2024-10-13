using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHp : MonoBehaviour
{
    public int health;

    private void Update()
    {
        if(health <= 0)
        {
            // death anim;
            Destroy(gameObject);
        }
    }
    public void HealthMinus(int count)
    {
        health -= count;
    }
}
