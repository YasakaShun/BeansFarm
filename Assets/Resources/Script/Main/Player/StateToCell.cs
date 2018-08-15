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
            var farms = Field.FieldManager.AllFarms()
                .Where(x => player.IsReachable(x.transform.position))
                .ToArray();

            if (farms.Any())
            {
                targetCell = farms[UnityEngine.Random.Range(0, farms.Length)];
                player.Agent.destination = targetCell.transform.position;
                player.Agent.stoppingDistance = 0.3f;
                return;
            }
            else
            {
                StateWait.ChangeState(player);
                return;
            }
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
