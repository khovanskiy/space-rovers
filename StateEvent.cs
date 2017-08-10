using System;
using Game.GCore;

namespace Game
{
    public class StateEvent : Event
    {
        public const String CHANGE_STATE = "changeState";
        public const String TURN_ON = "turnOn";
        public const String TURN_OFF = "turnOff";
        public String next;

        public StateEvent(EventDispatcher target, String type, String next = "") : base(target, type)
        {
            this.next = next;
        }
    }
}