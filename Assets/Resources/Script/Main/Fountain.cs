using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour
{
    private float power = 300.0f;
    private const float MaxPower = 300.0f;

    void Start()
    {
    }

    void Update()
    {
    }

    /// <summary>
    /// 水分取得。足りなければある分だけ。
    /// </summary>
    /// <param name="aPower"></param>
    /// <returns></returns>
    public float GetPower(float aPower = 10.0f)
    {
        float retVal = Mathf.Min(aPower, power);
        power -= retVal;

        return retVal;
    }

    /// <summary>
    /// 水分自動回復コルーチン。
    /// </summary>
    /// <returns></returns>
    private IEnumerator updatePower()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            power = Mathf.Min(power + 1.0f, MaxPower);
        }
    }
}
