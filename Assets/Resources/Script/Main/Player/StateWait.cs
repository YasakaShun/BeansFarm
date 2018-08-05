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
            player.Agent.enabled = false;

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
                    StateToCell.ChangeState(player);
                    yield break;
                }
                else
                {
                    var waters = GameObject.FindGameObjectsWithTag("Item")
                        .ToArray();

                    if (waters.Any())
                    {
                        var target = waters[UnityEngine.Random.Range(0, waters.Length)];
                        StateToWaterBall.ChangeState(player, target);

                        yield break;
                    }
                }
            }
        }

        Beans player;
    }
}
