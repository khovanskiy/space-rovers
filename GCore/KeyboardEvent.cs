using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.GCore
{
    public class KeyboardEvent : Event
    {
        public static readonly String KEY_DOWN = "keyDown";
        public static readonly String KEY_UP = "keyUp";

        private int _keyCode = 0;
        public KeyboardEvent(EventDispatcher target, String type, int keyCode=0) : base(target, type)
        {
            this._keyCode = keyCode;
        }
        public int keyCode
        {
            get
            {
                return _keyCode;
            }
        }
    }
}
