using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Obstacle;

namespace Obstacle
{
    class StateWait : IState
    {
        public static void ChangeState(Obj obj)
        {
            obj.ChangeState(new StateWait(obj));
        }

        private StateWait(Obj obj)
        {
            this.obj = obj;
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

        Obj obj;
    }
}
