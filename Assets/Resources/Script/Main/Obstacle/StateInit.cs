using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Obstacle;

namespace Obstacle
{
    class StateInit : IState
    {
        public static void ChangeState(Obj obj)
        {
            obj.ChangeState(new StateInit(obj));
        }

        private StateInit(Obj obj)
        {
            this.obj = obj;
        }

        public void OnStart()
        {
            StateWait.ChangeState(obj);
        }

        public void OnEnd()
        {

        }

        public void Update()
        {

        }

        Obj obj;
    }
}
