using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nut : MonoBehaviour
{
    public Sprite halfHpSprite;
    public Sprite lowHpSprite;

    private SpriteRenderer sRenderer;
    private PlantHp hp; //max hp = 1500

    private void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        hp = GetComponent<PlantHp>();
    }
    private void Update()
    {
        if(hp.health <= 700)
        {
            sRenderer.sprite = halfHpSprite;
        }
        if(hp.health <= 200)
        {
            sRenderer.sprite = lowHpSprite;
        }
    }
}
