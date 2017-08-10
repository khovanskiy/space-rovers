using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Ships
{
    public class ShipEvent : GCore.Event
    {
        public const String PART_CHANGE = "partChange";
        public ShipEvent(GCore.EventDispatcher target, String type)
            : base(target, type)
        {
            
        }
    }
}
