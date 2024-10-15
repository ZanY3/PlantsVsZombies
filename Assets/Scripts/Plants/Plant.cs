using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private GameObject cell;

    public void SetCell(GameObject cell)
    {
        this.cell = cell;
    }

    public void OnDestroy()
    {
        if (cell != null)
        {
            cell.GetComponent<SpriteRenderer>().enabled = true;
            cell.GetComponent<Cell>().canPlace = true;
        }
    }
}
