using System;

namespace Game.GCore
{
    public class Event
    {
        public const String COMPLETE = "complete";
        public const String ENTER_FRAME = "enterFrame";
        public const String CHANGE = "change";
        public const String ADDED_TO_STAGE = "addedToStage";
        public const String REMOVED_FROM_STAGE = "removedFromStage";

        private EventDispatcher _target;
        public String type;

        public Event(EventDispatcher target, String type)
        {
            this._target = target;
            this.type = type;
        }

        public EventDispatcher target
        {
            get { return _target; }
            set { _target = value; }
        }
    }
}