using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelMusicController : MonoBehaviour
{
    private void Start()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        // Поиск нужного объекта по имени
        foreach (GameObject obj in allObjects)
        {
            if (obj.tag == "MenuMusic")
            {
                // Уничтожение объекта
                Destroy(obj);
                break;
            }
        }
    }
}
