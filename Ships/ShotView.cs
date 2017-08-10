using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.GCore;

namespace Game.Ships
{
    public class ShotView : GCore.Bitmap
    {
        GCore.Timer timer = new GCore.Timer();
        public ShotView(Cannon model, GCore.Point from, GCore.Point to)
        {
            load("DATA\\Shots\\shot1.png");
            setAxis(0, height / 2);
            ratio = false;
            width = GCore.Point.distance(from, to);
            height = 5;
            rotationZ = GCore.Point.angle(from, to);
            x = from.x;
            y = from.y;

            timer.interval = 300;
            timer.addEventListener(GCore.TimerEvent.TIMER, onTimer);
            timer.start();
        }
        private void onTimer(GCore.Event e)
        {
            timer.stop();
            timer.removeEventListener(GCore.TimerEvent.TIMER, onTimer);
            this.visible = false;
            //dispatchEvent(new GCore.DisplayEvent(this, GCore.DisplayEvent.DELETE));
        }
    }
}
