using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public Transform prefab;
    public int width = 10;
    public int depth = 10;

    private const float CellWidth = 1.0f;

    public static GameObject GetRandomCell()
    {
        var cells = GameObject.FindGameObjectsWithTag("Cell");
        if (cells.Length == 0)
        {
            return null;
        }
        return cells[Random.Range(0, cells.Length)];
    }
	
}
