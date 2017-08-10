using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.GCore
{
    public interface IEventDispatcher
    {
        void addEventListener(String type,Function listener);
        void removeEventListener(String type,Function listener);
        void dispatchEvent(Event e);
        bool hasEventListener(String type);
    }
}
