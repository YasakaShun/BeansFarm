using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Resource アクセスの仲介
/// </summary>
public class PrefabManager
{
    public static GameObject Player = (GameObject)Resources.Load("Prefab/Main/unitychan");
    public static Material FarmMaterial = (Material)Resources.Load("Material/Main/Field/CellFarm");
    public static Material FountainMaterial = (Material)Resources.Load("Material/Main/Field/CellFountain");
    public static GameObject WaterBall = (GameObject)Resources.Load("Prefab/Main/WaterBall");
}
