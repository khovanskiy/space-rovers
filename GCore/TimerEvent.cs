using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.GCore
{
    public class TimerEvent : Event
    {
        public const String TIMER = "timer";
        public TimerEvent(EventDispatcher target, String type) : base(target,type)
        {
            
        }
    }
}
