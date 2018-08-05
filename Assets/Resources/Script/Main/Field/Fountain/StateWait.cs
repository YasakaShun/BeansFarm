using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Field.Fountain
{
    class StateWait : IState
    {
        public static void ChangeState(Cell cell)
        {
            cell.ChangeState(new StateWait(cell));
        }

        private StateWait(Cell cell)
        {
            this.cell = cell;
        }

        public void OnStart()
        {
            cell.StartCoroutine(createWaterBall());
        }

        public void OnEnd()
        {
            cell.StopCoroutine(createWaterBall());
        }

        public void Update()
        {

        }

        /// <summary>
        /// 水分自動排出コルーチン。
        /// </summary>
        /// <returns></returns>
        private IEnumerator createWaterBall()
        {
            yield return new WaitForSeconds(1.0f);
            while (true)
            {
                // TODO: 空いてる場所にWaterBallを生む
                createWaterBall(10.0f);
                yield return new WaitForSeconds(5.0f);
            }
        }

        /// <summary>
        /// WaterBall生成
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        private GameObject createWaterBall(float power)
        {
            var offs = Quaternion.AngleAxis(UnityEngine.Random.Range(0.0f, 360.0f), Vector3.up) * Vector3.forward;
            var waterBall = UnityEngine.Object.Instantiate(
                PrefabManager.WaterBall,
                cell.transform.position + Vector3.up + offs,
                Quaternion.identity
                );
            waterBall.GetComponent<WaterBall>().Power = power;

            return waterBall;
        }

        Cell cell;
    }
}
