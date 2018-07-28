﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public Transform cell;
    public int width = 10;
    public int depth = 10;

    private const float CellWidth = 10.0f;

    void Start()
    {
        // フィールド生成
        float hw = width * CellWidth * 0.5f;
        float hd = depth * CellWidth * 0.5f;
        for (float w = -hw + CellWidth * 0.5f; w < hw; w += CellWidth)
        {
            for (float d = -hd + CellWidth * 0.5f; d < hd; d += CellWidth)
            {
                var pos = new Vector3(w, CellWidth * -0.5f, d);
                Instantiate(cell, pos, new Quaternion());
            }

        }
	}
	
}
