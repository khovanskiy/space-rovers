using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    interface IFactory
    {
        State create(String name);
    }
}
