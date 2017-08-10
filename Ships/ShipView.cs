using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.GCore;

namespace Game
{
    public class ShipView : MovieClip
    {
        Ships.Ship _model;
        public ShipView(Ships.Ship model)
        {
            _model = model;
            _model.addEventListener(Event.CHANGE, onChange);
            this.load(new string[]{"DATA\\Ships\\3.png"});
            this.stop();
            this.moveAxisToCenter();
            this.width = 50;
        }
        ~ShipView()
        {
            _model.removeEventListener(Event.CHANGE, onChange);
        }
        public void onChange(Event e)
        {
            this.x = _model.x;
            this.y = _model.y;
            this.rotationZ = _model.angle;
        }
    }
}
