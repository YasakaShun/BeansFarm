using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    class StateToSource : IState
    {
        public static void ChangeState(Beans player)
        {
            player.ChangeState(new StateToSource(player));
        }

        private StateToSource(Beans player)
        {
            this.player = player;
        }

        public void OnStart()
        {
            player.Agent.enabled = true;

            targetFountain = Source.GetRandomFountain();
            var pos = targetFountain.transform.position;
            pos.y = 0;
            player.Agent.destination = pos;
            player.Agent.stoppingDistance = targetFountain.transform.localScale.x * 0.5f + player.Agent.radius;
        }

        public void OnEnd()
        {

        }

        public void Update()
        {
            if (player.isReached())
            {
                StateGatherWater.ChangeState(player, targetFountain);
            }
        }

        private Beans player;
        private GameObject targetFountain;
    }
}
