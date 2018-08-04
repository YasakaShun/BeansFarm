using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    class StateTemplate : IState
    {
        public static void ChangeState(Beans player)
        {
            player.ChangeState(new StateTemplate(player));
        }

        private StateTemplate(Beans player)
        {
            this.player = player;
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

        private Beans player;
    }
}
