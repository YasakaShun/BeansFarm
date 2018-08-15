using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    class StateAttack : IState
    {
        public static void ChangeState(Beans player, GameObject target)
        {
            player.ChangeState(new StateAttack(player, target));
        }

        private StateAttack(Beans player, GameObject target)
        {
            this.player = player;
            this.targetObstacle = target;
        }

        public void OnStart()
        {
            player.Agent.ResetPath();
            player.StartCoroutine(Attack());
        }

        public void OnEnd()
        {

        }

        public void Update()
        {

        }

        private IEnumerator Attack()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);

                if (targetObstacle != null)
                {
                    targetObstacle.GetComponent<Obstacle.Obj>().Damage(1);

                    yield return new WaitForSeconds(0.5f);

                    if (targetObstacle == null)
                    {
                        StateWait.ChangeState(player);
                        yield break;
                    }
                }
                else
                {
                    // 何らかの理由で目標の Obstacle がなくなったらWaitへ
                    StateWait.ChangeState(player);
                    yield break;
                }
            }
        }

        private Beans player;
        private GameObject targetObstacle;
    }
}
