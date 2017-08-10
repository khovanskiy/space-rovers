using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.GCore
{
    public class Event
    {
        public static readonly String COMPLETE = "complete";
        public static readonly String ENTER_FRAME = "enterFrame";
        public static readonly String CHANGE = "change";
        public static readonly String ADDED_TO_STAGE = "addedToStage";
        public static readonly String REMOVED_FROM_STAGE = "removedFromStage";

        private EventDispatcher _target;
        public String type;

        public Event(EventDispatcher target, String type)
        {
            this._target = target;
            this.type = type;
        }
        public EventDispatcher target
        {
            get
            {
                return _target;
            }
            set
            {
                _target = value;
            }
        }
    }
}
