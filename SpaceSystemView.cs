using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Astronomy.Objects;
using Game.GCore;

namespace Game
{
    public class SpaceSystemView : Sprite
    {
        private SpaceObject _model;
        public SpaceSystemView(SpaceObject model)
        {
            _model = model;
            model.addEventListener(Event.CHANGE, onNextTick);
        }
        public void onNextTick(Event e)
        {
            //Console.WriteLine(_model.spaceobject_name + " CHANGE " + _model.local_angle);
            this.x = _model.local_x;
            this.y = _model.local_y;
            this.rotationZ = _model.local_angle;
        }
    }
}
