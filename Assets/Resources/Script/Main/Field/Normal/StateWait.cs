using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Field.Normal
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
