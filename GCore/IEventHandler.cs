using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.GCore
{
    public interface IEventHandler
    {
        void handleEvent(Event e);
        void follow(String type, EventDispatcher ed);
    }
}
