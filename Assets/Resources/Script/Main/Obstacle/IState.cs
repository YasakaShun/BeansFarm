using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Obstacle
{
    public interface IState
    {
        void OnStart();
        void OnEnd();
        void Update();
    }
}
