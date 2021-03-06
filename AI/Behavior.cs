﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.AI
{
    public abstract class Behavior
    {
        protected int _status;
        protected int _priority;
        virtual public void init()
        {
            _priority = 0;
        }
        public int priority
        {
            get
            {
                return _priority;
            }
            set
            {
                _priority = value;
            }
        }
        protected abstract int execute();
        public int tick()
        {
            _status = execute();
            return _status;
        }
    }
}
