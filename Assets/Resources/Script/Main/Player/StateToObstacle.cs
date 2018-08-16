using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    class StateToObstacle : IState
    {
        public static bool TryToChangeState(Beans player)
        {
            // 到達可能な Obstacle を目指す
            var obstacles = GameObject.FindGameObjectsWithTag("Obstacle")
                .ToList();
            var reachables = new List<GameObject>();

            foreach (var obstacle in obstacles)
            {
                var hit = new UnityEngine.AI.NavMeshHit();
                bool ret = player.Agent.Raycast(obstacle.transform.position, out hit);
                var diff = (hit.position - obstacle.transform.position);
                diff.y = 0.0f;
                if (diff.magnitude <= 1.0f) {
                    reachables.Add(obstacle);
                }
            }

            if (reachables.Any())
            {
                var target = obstacles[UnityEngine.Random.Range(0, reachables.Count)];
                StateToObstacle.ChangeState(player, target);
                return true;
            }

            return false;
        }

        public static void ChangeState(Beans player, GameObject target)
        {
            player.ChangeState(new StateToObstacle(player, target));
        }

        private StateToObstacle(Beans player, GameObject target)
        {
            this.player = player;
            this.targetObstacle = target;
        }

        public void OnStart()
        {
            var pos = targetObstacle.transform.position;
            pos.y = 0;
            player.Agent.destination = pos;
            player.Agent.stoppingDistance = 0.5f;
        }

        public void OnEnd()
        {

        }

        public void Update()
        {
            // 何らかの理由で目標の Obstacle がなくなった
            if (targetObstacle == null)
            {
                StateWait.ChangeState(player);
                return;
            }

            // 到着
            if (player.Agent.enabled && player.isReached())
            {
                StateAttack.ChangeState(player, targetObstacle);
                return;
            }
        }

        public bool CanReceiveSignal(Signal signal)
        {
            switch (signal)
            {
                case Signal.ToFarm:
                    return false;
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
                    break;
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

        private Beans player;
        private GameObject targetObstacle;
    }
}
