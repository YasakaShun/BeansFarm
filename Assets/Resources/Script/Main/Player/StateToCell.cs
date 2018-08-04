using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    class StateToCell : IState
    {
        public static void ChangeState(Beans player, GameObject waterBall)
        {
            player.ChangeState(new StateToCell(player, waterBall));
        }

        private StateToCell(Beans player, GameObject waterBall)
        {
            this.player = player;
            this.waterBall = waterBall;
        }

        public void OnStart()
        {
            player.Agent.enabled = true;

            targetCell = Field.GetRandomCell();
            player.Agent.destination = targetCell.transform.position;
            player.Agent.stoppingDistance = 0;
        }

        public void OnEnd()
        {

        }

        public void Update()
        {
            if (player.isReached())
            {
                StateUseWater.ChangeState(player, waterBall, targetCell);
            }
        }

        private Beans player;
        private GameObject waterBall;
        private GameObject targetCell;
    }
}
