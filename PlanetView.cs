using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.GCore;

namespace Game
{
    public class PlanetView : Bitmap
    {
        private Astronomy.SpaceObject _model;
        public PlanetView(Astronomy.SpaceObject model)
        {
            _model = model;
            model.addEventListener(Event.CHANGE, onNextTick);
            this.setRSPointToCenter();
        }
        public void onNextTick(Event e)
        {
            this.x = _model.local_x;
            this.y = _model.local_y;
            this.rotationZ = _model.local_angle;
        }
    }
}
