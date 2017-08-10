using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game.GCore;

namespace Game
{
    public class GameIntroState : State
    {
        //MovieClip m;
        TextField tf;
        public GameIntroState()
        {
            
        }
        override public void init()
        {
            //m = new MovieClip();
            tf = new TextField("DLK");
            tf.size = 30;
            tf.x = (1024 - tf.width) / 2;
            tf.y = (600 - tf.height) / 2;
            //m.load(new string[] { "L:\\1\\exo\\anim0001.png", "L:\\1\\exo\\anim0002.png", "L:\\1\\exo\\anim0003.png", "L:\\1\\exo\\anim0004.png", "L:\\1\\exo\\anim0005.png", "L:\\1\\exo\\anim0006.png", "L:\\1\\exo\\anim0007.png", "L:\\1\\exo\\anim0008.png", "L:\\1\\exo\\anim0009.png", "L:\\1\\exo\\anim0010.png" });
            Game.background.addChild(tf);
            dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "menu"));
            tf.addEventListener(MouseEvent.MOUSE_MOVE, onClick);
        }
        private void onClick(Event e)
        {
            Console.WriteLine("Click");
            dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "menu"));
        }
        override public void render()
        {
            
        }
        override public void release()
        {
            Game.background.removeChild(tf);
        }
    }
}
