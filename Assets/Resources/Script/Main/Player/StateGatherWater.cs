﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    class StateGatherWater : IState
    {
        public static void ChangeState(Beans player, GameObject fountain)
        {
            player.ChangeState(new StateGatherWater(player, fountain));
        }

        private StateGatherWater(Beans player, GameObject fountain)
        {
            this.player = player;
            this.fountain = fountain;
        }

        public void OnStart()
        {
            player.Agent.enabled = false;
            player.StartCoroutine(gatherWater());
        }

        public void OnEnd()
        {

        }

        public void Update()
        {

        }

        private IEnumerator gatherWater()
        {
            yield return new WaitForSeconds(1);

            Debug.Assert(fountain != null);

            var power = fountain.GetComponent<Fountain>().GetPower();
            var waterBall = createWaterBall(power);

            StateToCell.ChangeState(player, waterBall);
        }

        private GameObject createWaterBall(float power)
        {
            var prefab = (GameObject)Resources.Load("Prefab/Main/WaterBall");
            var waterBall = UnityEngine.Object.Instantiate(
                prefab,
                player.transform.position + Vector3.up * 2,
                Quaternion.identity,
                player.transform
                );
            waterBall.GetComponent<WaterBall>().power = power;

            return waterBall;
        }

        private Beans player;
        private GameObject fountain;
    }
}
