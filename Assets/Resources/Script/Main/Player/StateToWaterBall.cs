using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    class StateToWaterBall : IState
    {
        public static bool TryToChangeState(Beans player)
        {
            // 到達可能なWaterBallを目指す
            var waters = GameObject.FindGameObjectsWithTag("Item")
                .Where(x => x.GetComponent<WaterBall>().Parent == null)
                .Where(x => player.IsReachable(x.transform.position))
                .ToArray();

            if (waters.Any())
            {
                var target = waters[UnityEngine.Random.Range(0, waters.Length)];
                StateToWaterBall.ChangeState(player, target);
                return true;
            }

            return false;
        }

        public static void ChangeState(Beans player, GameObject waterBall)
        {
            player.ChangeState(new StateToWaterBall(player, waterBall));
        }

        private StateToWaterBall(Beans player, GameObject waterBall)
        {
            this.player = player;
            this.targetWaterBall = waterBall;
        }

        public void OnStart()
        {
            var pos = targetWaterBall.transform.position;
            pos.y = 0;
            player.Agent.destination = pos;
            player.Agent.stoppingDistance = 0.5f;
        }

        public void OnEnd()
        {

        }

        public void Update()
        {
            if (player.hasWaterBall())
            {
                StateWait.ChangeState(player);
                return;
            }

            if (targetWaterBall == null)
            {
                StateWait.ChangeState(player);
                return;
            }

            if (targetWaterBall.GetComponent<WaterBall>().Parent != null)
            {
                StateWait.ChangeState(player);
                return;
            }

            if (player.Agent.enabled && player.isReached())
            {
                StateWait.ChangeState(player);
                return;
            }
        }

        private Beans player;
        private GameObject targetWaterBall;
    }
}
