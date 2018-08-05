using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    class StateUseWater : IState
    {
        public static void ChangeState(Beans player, GameObject targetCell)
        {
            player.ChangeState(new StateUseWater(player, targetCell));
        }

        private StateUseWater(Beans player, GameObject targetCell)
        {
            this.player = player;
            this.targetCell = targetCell;
        }

        public void OnStart()
        {
            player.Agent.enabled = false;

            player.StartCoroutine(useWater());
        }

        public void OnEnd()
        {

        }

        public void Update()
        {

        }

        private IEnumerator useWater()
        {
            yield return new WaitForSeconds(1);

            if (targetCell != null)
            {
                if (player.hasWaterBall())
                {
                    var cellScript = targetCell.GetComponent<Field.Cell>();
                    cellScript.waterPower +=
                        player.WaterBall.GetComponent<WaterBall>().Power;
                    if (30.0f < cellScript.waterPower)
                    {
                        var prefab = (GameObject)Resources.Load("Prefab/Main/unitychan");
                        UnityEngine.Object.Instantiate(prefab, player.transform);
                    }
                    UnityEngine.Object.Destroy(player.WaterBall);
                    player.WaterBall = null;
                }
                targetCell = null;

                StateWait.ChangeState(player);
            }
            else
            {
                if (player.hasWaterBall())
                {
                    StateToCell.ChangeState(player);
                }

            }
        }

        private Beans player;
        private GameObject targetCell;
    }
}
