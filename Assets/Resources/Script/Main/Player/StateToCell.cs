using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    class StateToCell : IState
    {
        public static bool TryToChangeState(Beans player)
        {
            // 移動可能な畑を目指す
            var farms = Field.FieldManager.AllFarms()
                .Where(x => player.IsReachable(x.transform.position))
                .ToArray();

            if (farms.Any())
            {
                var targetCell = farms[UnityEngine.Random.Range(0, farms.Length)];
                StateToCell.ChangeState(player, targetCell);
                return true;
            }

            return false;
        }

        public static void ChangeState(Beans player, GameObject targetCell)
        {
            player.ChangeState(new StateToCell(player, targetCell));
        }

        private StateToCell(Beans player, GameObject targetCell)
        {
            this.player = player;
            this.targetCell = targetCell;
        }

        public void OnStart()
        {
            player.Agent.destination = targetCell.transform.position;
            player.Agent.stoppingDistance = 0.3f;
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

        public bool CanReceiveSignal(Signal signal)
        {
            return false;
        }

        public void OnReceiveSignal(Signal signal, GameObject gameObject)
        {
        }

        private Beans player;
        private GameObject targetCell;
    }
}
