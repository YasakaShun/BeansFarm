using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;

    void FixedUpdate()
    {
        // 入力をxとzに代入
        float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;

        // 同一のGameObjectが持つRigidbodyコンポーネントを取得
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(x, 0, z);
    }

}
