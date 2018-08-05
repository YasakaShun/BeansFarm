using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : MonoBehaviour
{
    public float Power { get; set; }
    public Player.Beans Parent { get; set; }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Player"))
        {
            var player = hit.gameObject.GetComponent<Player.Beans>();
            if (player != null)
            {
                if (player.hasWaterBall())
                {
                    return;
                }
                Parent = player;
                player.WaterBall = this.gameObject;
                transform.position = player.transform.position + Vector3.up * 2;
                transform.rotation = Quaternion.identity;
                transform.parent = player.transform;
            }
        }
    }

}
