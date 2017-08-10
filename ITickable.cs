using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    interface ITickable
    {
        void nextTick(float dt);
    }
}
