using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // ターゲットへの参照
    public Transform target;
    private Vector3 offset;

    void Start()
    {
        offset = GetComponent<Transform>().position - target.position;
    }

    void Update()
    {
        var trans = GetComponent<Transform>();
        trans.position = target.position + offset;
		
	}
}
