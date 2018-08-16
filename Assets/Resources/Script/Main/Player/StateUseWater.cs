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
            mCoroutine = useWater();

            player.Agent.ResetPath();
            player.StartCoroutine(mCoroutine);
        }

        public void OnEnd()
        {
            player.StopCoroutine(mCoroutine);
        }

        public void Update()
        {

        }

        public bool CanReceiveSignal(Signal signal)
        {
            return false;
        }

        public void OnReceiveSignal(Signal signal, GameObject gameObject)
        {
        }

        private IEnumerator useWater()
        {
            yield return new WaitForSeconds(0.5f);

            if (targetCell != null)
            {
                if (player.hasWaterBall())
                {
                    var cellScript = targetCell.GetComponent<Field.Cell>();
                    cellScript.GiveWater(player.WaterBall.GetComponent<WaterBall>());
                    UnityEngine.Object.Destroy(player.WaterBall);
                    player.WaterBall = null;
                }
                targetCell = null;

                yield return new WaitForSeconds(0.5f);

                StateWait.ChangeState(player);
                yield break;
            }
            else
            {
                StateWait.ChangeState(player);
                yield break;
            }
        }

        private Beans player;
        private GameObject targetCell;
        private IEnumerator mCoroutine;
    }
}
