using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Source : MonoBehaviour
{
    public static GameObject GetRandomFountain()
    {
        var fountains = GameObject.FindGameObjectsWithTag("Source");
        if (fountains.Length == 0)
        {
            return null;
        }
        return fountains[Random.Range(0, fountains.Length)];
    }
}
