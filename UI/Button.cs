using System;
using Game.GCore;

namespace Game.UI
{
    public class Button : MovieClip
    {
        public Button()
        {
            stop();
        }

        ~Button()
        {
            Console.WriteLine("Удалили " + id);
        }

        protected override void onMouseOver(Event e)
        {
            gotoAndStop(1);
        }

        protected override void onMouseOut(Event e)
        {
            gotoAndStop(0);
        }
    }
}