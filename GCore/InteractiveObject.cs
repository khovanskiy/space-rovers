using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.GCore
{
    public abstract class InteractiveObject : DisplayObject 
    {
        virtual protected void handleEvent(Event e)
        {

        }
        public void follow(String type, EventDispatcher ed)
        {
            ed.addEventListener(type, handleEvent);
        }
        public void unfollow(String type, EventDispatcher ed)
        {
            ed.removeEventListener(type, handleEvent);
        }
    }
}
