using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SunManager : MonoBehaviour
{
    public int count;
    public TMP_Text sunText;

    private void Start()
    {
        sunText.text = count.ToString();
    }

    public void SunPlus(int value)
    {
        count += value;
        sunText.text = count.ToString();
    }
    public void SunMinus(int value)
    {
        count -= value;
        sunText.text = count.ToString();
    }
}
