using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    class StateWait : IState
    {
        public static void ChangeState(Beans player)
        {
            player.ChangeState(new StateWait(player));
        }

        private StateWait(Beans player)
        {
            this.player = player;
        }

        public void OnStart()
        {
            player.Agent.ResetPath();
            player.StartCoroutine(wait());
        }

        public void OnEnd()
        {

        }

        public void Update()
        {

        }

        private IEnumerator wait()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);

                if (player.hasWaterBall())
                {
                    if (StateToCell.TryToChangeState(player))
                    {
                        yield break;
                    }
                }
                else
                {
                    if (StateToWaterBall.TryToChangeState(player))
                    {
                        yield break;
                    }
                }
            }
        }

        Beans player;
    }
}
