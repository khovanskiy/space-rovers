using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Game.GCore
{
    public class Keyboard : EventDispatcher
    {
        public static readonly int ENTER = 13;
        public static readonly int SPACE = 32;
        public static readonly int ESCAPE = 27;
        public static readonly int F5 = 116;
        public static readonly int F6 = 117;
        static Keyboard _instance = null;
        private Keyboard()
        {
            var core = GraphicCore.getInstance();
            core.form.KeyDown += (sender, args) =>
            {
                dispatchEvent(new KeyboardEvent(this, KeyboardEvent.KEY_DOWN,args.KeyValue));
            };
            core.form.KeyUp += (sender, args) =>
            {
                dispatchEvent(new KeyboardEvent(this, KeyboardEvent.KEY_UP, args.KeyValue));
            };
        }
        public static Keyboard getInstance()
        {
            if (_instance == null)
            {
                _instance = new Keyboard();
            }
            return _instance;
        }
    }
}
