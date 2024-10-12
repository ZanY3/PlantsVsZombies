using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public int addValue;
    public GameObject takeParticles;

    private SunManager sunManager;
    private void Start()
    {
        sunManager = FindAnyObjectByType<SunManager>();
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
