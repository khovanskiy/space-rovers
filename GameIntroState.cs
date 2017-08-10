using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game.Engine;

namespace Game
{
    public class GameIntroState : State
    {
        MovieClip m;
        public GameIntroState()
        {
            m = new MovieClip();
            m.load(new string[] { "L:\\1\\exo\\anim0001.png", "L:\\1\\exo\\anim0002.png", "L:\\1\\exo\\anim0003.png", "L:\\1\\exo\\anim0004.png", "L:\\1\\exo\\anim0005.png", "L:\\1\\exo\\anim0006.png", "L:\\1\\exo\\anim0007.png", "L:\\1\\exo\\anim0008.png", "L:\\1\\exo\\anim0009.png", "L:\\1\\exo\\anim0010.png" });
            Game.background.addChild(m);
            m.frameRate = 10;
            m.addEventListener(MouseEvent.CLICK, onClick);
        }
        private void onClick(Event e)
        {
            Console.WriteLine("Click");
            dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "menu"));
        }
        override public void logic()
        {
            
        }
        override public void release()
        {
            Game.background.removeChild(m);
        }
    }
}
