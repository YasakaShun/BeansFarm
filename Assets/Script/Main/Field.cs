using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public Transform cell;
    public int width = 10;
    public int depth = 10;

    private const float CellHW = 0.5f;
    private const float CellHD = 0.5f;

    void Start()
    {
        // フィールド生成
        float hw = width / 2.0f;
        float hd = depth / 2.0f;
        for (float w = -hw + CellHW; w < hw; w += 1.0f)
        {
            for (float d = -hd + CellHD; d < hd; d += 1.0f)
            {
                var pos = new Vector3(w, -1.0f, d);
                Instantiate(cell, pos, new Quaternion());
            }

        }
	}
	
}
