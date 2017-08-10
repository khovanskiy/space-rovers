using System;

namespace Game.GCore
{
    public class MouseEvent : Event
    {
        public const String CLICK = "click";
        public const String MOUSE_MOVE = "mouseMove";
        public const String MOUSE_WHEEL = "mouseWheel";
        public const String MOUSE_UP = "mouseUp";
        public const String MOUSE_DOWN = "mouseDown";
        public const String MOUSE_OVER = "mouseOver";
        public const String MOUSE_OUT = "mouseOut";

        private bool _buttonDown;
        private int _localX;
        private int _localY;
        private int _delta;

        public MouseEvent(EventDispatcher target, String type, int localX = 0, int localY = 0, bool buttonDown = false,
            int delta = 0) : base(target, type)
        {
            _localX = localX;
            _localY = localY;
            _buttonDown = buttonDown;
            _delta = delta;
        }

        public int localX
        {
            get { return _localX; }
        }

        public int localY
        {
            get { return _localY; }
        }

        public int delta
        {
            get { return _delta; }
        }

        public bool buttonDown
        {
            get { return _buttonDown; }
        }
    }
}