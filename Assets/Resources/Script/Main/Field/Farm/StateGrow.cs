using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Field.Farm
{
    class StateGrow : IState
    {
        public static void ChangeState(Cell cell)
        {
            cell.ChangeState(new StateGrow(cell));
        }

        private StateGrow(Cell cell)
        {
            this.cell = cell;
        }

        public void OnStart()
        {
            cell.StartCoroutine(Grow());
        }

        public void OnEnd()
        {

        }

        public void Update()
        {
        }

        private IEnumerator Grow()
        {
            yield return new WaitForSeconds(30.0f);

            if (Constant.GenWaterPower <= cell.waterPower)
            {
                // Player 生成
                CreatePlayer();
                StateWait.ChangeState(cell);
                yield break;
            }
            else
            {
                // 再びGrowへ。
                // TODO: 周りに水分を撒くなど仕様を考える
                StateGrow.ChangeState(cell);
                yield break;
            }
        }

        private GameObject CreatePlayer()
        {
            var prefab = PrefabManager.Player;
            var pos = cell.transform.position;
            pos.y = 0.0f;
            var player = UnityEngine.Object.Instantiate(prefab, pos, Quaternion.identity);
            player.GetComponent<Player.Beans>().Life = (int)cell.waterPower;
            cell.waterPower = 0.0f;
            return player;
        }

        Cell cell;
    }
}
