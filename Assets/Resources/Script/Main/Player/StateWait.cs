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
            mCoroutine = wait();

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
            switch (signal)
            {
                case Signal.ToFarm:
                    if (player.hasWaterBall())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case Signal.ToObstacle:
                case Signal.ToWaterBall:
                    return true;
                default:
                    throw new NotImplementedException();
            }
        }

        public void OnReceiveSignal(Signal signal, GameObject gameObject)
        {
            switch (signal)
            {
                case Signal.ToFarm:
                    Debug.Assert(player.hasWaterBall());
                    StateToCell.ChangeState(player, gameObject);
                    return;
                case Signal.ToObstacle:
                    StateToObstacle.ChangeState(player, gameObject);
                    return;
                case Signal.ToWaterBall:
                    StateToWaterBall.ChangeState(player, gameObject);
                    return;
                default:
                    throw new NotImplementedException();
            }
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
                    bool toObstacle = (0.8f <= UnityEngine.Random.Range(0.0f, 1.0f));
                    if (toObstacle)
                    {
                        // 障害物の破壊へ
                        if (StateToObstacle.TryToChangeState(player))
                        {
                            yield break;
                        }
                        if (StateToWaterBall.TryToChangeState(player))
                        {
                            yield break;
                        }
                    }
                    else
                    {
                        // 水分取得へ
                        if (StateToWaterBall.TryToChangeState(player))
                        {
                            yield break;
                        }
                    }
                }
            }
        }

        private Beans player;
        private IEnumerator mCoroutine;
    }
}
