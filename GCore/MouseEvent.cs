using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.GCore
{
    public class MouseEvent:Event
    {
        public static readonly String CLICK = "click";
        public static readonly String MOUSE_MOVE = "mouseMove";
        public static readonly String MOUSE_WHEEL = "mouseWheel";
        public static readonly String MOUSE_UP = "mouseUp";
        public static readonly String MOUSE_DOWN = "mouseDown";
        public static readonly String MOUSE_OVER = "mouseOver";
        public static readonly String MOUSE_OUT = "mouseOut";

        private bool _buttonDown = false;
        private int _localX = 0;
        private int _localY = 0;
        private int _delta = 0;
        public MouseEvent(EventDispatcher target, String type,int localX=0,int localY=0,bool buttonDown=false,int delta=0) : base(target,type)
        {
            this._localX = localX;
            this._localY = localY;
            this._buttonDown = buttonDown;
            this._delta = delta;
        }
        public int localX
        {
            get
            {
                return _localX;
            }
        }
        public int localY
        {
            get
            {
                return _localY;
            }
        }
        public int delta
        {
            get
            {
                return _delta;
            }
        }
        public bool buttonDown
        {
            get
            {
                return _buttonDown;
            }
        }
    }
}
