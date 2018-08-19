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
            mCoroutine = Attack();

            player.Agent.ResetPath();
            player.StartCoroutine(mCoroutine);
        }

        public void OnEnd()
        {
            player.StopCoroutine(mCoroutine);
            player.Anim.SetBool("Jump", false);

            if (player.Signal == Signal.ToObstacle)
            {
                player.ResetSignal();
            }
        }

        public void Update()
        {
            // Speed を設定しないとジャンプモーションしてくれない。
            // TODO: 正式対応
            player.Anim.SetFloat("Speed", 1.0f);
            player.Anim.SetBool("Jump", true);
        }

        public bool CanReceiveSignal(Signal signal)
        {
            return false;
        }

        public void OnReceiveSignal(Signal signal, GameObject gameObject)
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
        private IEnumerator mCoroutine;
    }
}
