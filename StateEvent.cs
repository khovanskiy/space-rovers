using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Game.GCore;

namespace Game
{
    public class StateEvent : Event
    {
        public const String CHANGE_STATE = "changeState";
        public const String TURN_ON = "turnOn";
        public const String TURN_OFF = "turnOff";
        public String next = "";
        public Hashtable args = null;
        public StateEvent(EventDispatcher target, String type, String next = "", Hashtable table = null)
            : base(target, type)
        {
            this.next = next;
            this.args = table;
        }
    }
}
