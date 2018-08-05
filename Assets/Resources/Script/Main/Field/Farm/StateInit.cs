﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Field.Farm
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
            cell.GetComponent<MeshRenderer>().material = PrefabManager.FarmMaterial;
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
