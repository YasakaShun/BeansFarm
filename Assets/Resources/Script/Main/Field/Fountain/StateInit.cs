using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Field.Fountain
{
    class StateInit : IState
    {
        public static void ChangeState(Cell cell)
        {
            cell.ChangeState(new StateInit(cell));
        }

        private StateInit(Cell cell)
        {
            this.cell = cell;
        }

        public void OnStart()
        {
            cell.custom = new Custom();
            cell.GetComponent<MeshRenderer>().material = PrefabManager.FountainMaterial;
            StateWait.ChangeState(cell);
        }

        public void OnEnd()
        {

        }

        public void Update()
        {

        }

        Cell cell;
    }
}
