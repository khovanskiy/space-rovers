using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.GCore;

namespace Game
{
    public class GameTestState : State
    {
        Sprite sp;
        TextField console = new TextField("");
        Bitmap m1;
        public override void init()
        {
            console.size = 25;
            keeper.add(Game.interfaceView, console);

            sp = new Sprite();
            keeper.add(Game.interfaceView, sp);
            m1 = new Bitmap();
            m1.loadAsync("L:\\test_image.jpg");
            sp.addChild(m1);
            /*Bitmap dot2 = new Bitmap();
            dot2.loadAsync("DATA\\Other\\dot.png");
            
            sp.addChild(m1);
            sp.addChild(dot2);
            

            m1.x = 50;
            m1.y = 50;
            sp.x = 50;
            sp.y = 50;
            println(sp._width + " " + m1.width + " " +m1.scaleX);/**/

            /*Bitmap[] arr = new Bitmap[5];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = new Bitmap();
                arr[i].loadAsync("L:\\asyncTest\\"+(i+1)+".jpg");
                sp.addChild(arr[i]);
                arr[i].x = i * 150;
            }*/
            keeper.add(Game.keyboard, KeyboardEvent.KEY_UP, onKeyUp);
        }
        private void clear()
        {
            console.text = "";
        }
        private void print(String s)
        {
            console.text += s;
        }
        private void println(String s)
        {
            print(s);
            console.text += "\n";
        }
        private void onKeyUp(Event e)
        {
            KeyboardEvent ev = (KeyboardEvent)e;
            if (ev.keyCode == Keyboard.ESCAPE)
            {
                dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "exit"));
            }
        }
        public override void render()
        {
            sp.spoilDown();
            //sp.alpha -= 0.00001f;
            clear();
            println(GraphicCore.TOTAL_COUNT+"");
        }
    }
}
