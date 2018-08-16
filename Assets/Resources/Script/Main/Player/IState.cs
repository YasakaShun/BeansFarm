using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Player
{
    public interface IState
    {
        void OnStart();
        void OnEnd();
        void Update();

        bool CanReceiveSignal(Signal signal);
        void OnReceiveSignal(Signal signal, GameObject gameObject);
    }
}
