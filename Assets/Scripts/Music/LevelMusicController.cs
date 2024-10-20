using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelMusicController : MonoBehaviour
{
    private void Start()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        // ����� ������� ������� �� �����
        foreach (GameObject obj in allObjects)
        {
            if (obj.tag == "MenuMusic")
            {
                // ����������� �������
                Destroy(obj);
                break;
            }
        }
    }
}
