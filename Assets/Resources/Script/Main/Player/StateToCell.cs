using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    class StateToCell : IState
    {
        public static void ChangeState(Beans player)
        {
            player.ChangeState(new StateToCell(player));
        }

        private StateToCell(Beans player)
        {
            this.player = player;
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
                StateUseWater.ChangeState(player, targetCell);
            }
        }

        private Beans player;
        private GameObject targetCell;
    }
}
