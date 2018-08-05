using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainOld : MonoBehaviour
{
    public float power { get; private set; }
    public readonly float MaxPower = 300.0f;

    void Start()
    {
        power = 100.0f;

        StartCoroutine(UpdatePower());
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
    private IEnumerator UpdatePower()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            power = Mathf.Min(power + 1.0f, MaxPower);
        }
    }
}
