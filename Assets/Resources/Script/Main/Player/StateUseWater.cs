using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    class StateUseWater : IState
    {
        public static void ChangeState(Beans player, GameObject waterBall, GameObject targetCell)
        {
            player.ChangeState(new StateUseWater(player, waterBall, targetCell));
        }

        private StateUseWater(Beans player, GameObject waterBall, GameObject targetCell)
        {
            this.player = player;
            this.waterBall = waterBall;
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
                if (waterBall != null)
                {
                    var cellScript = targetCell.GetComponent<Cell>();
                    cellScript.waterPower +=
                        waterBall.GetComponent<WaterBall>().power;
                    if (30.0f < cellScript.waterPower)
                    {
                        var prefab = (GameObject)Resources.Load("Prefab/Main/unitychan");
                        UnityEngine.Object.Instantiate(prefab, player.transform);
                    }
                    UnityEngine.Object.Destroy(waterBall);
                    waterBall = null;
                }
                targetCell = null;

                StateWait.ChangeState(player);
            }
            else
            {
                if (waterBall != null)
                {
                    StateToCell.ChangeState(player, waterBall);
                }

            }
        }

        private Beans player;
        private GameObject waterBall;
        private GameObject targetCell;
    }
}
