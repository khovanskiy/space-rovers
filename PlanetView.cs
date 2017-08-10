using System;
using Game.GCore;

namespace Game
{
    public class PlanetView : Bitmap
    {
        private Astronomy.SpaceObject _model;

        public PlanetView(Astronomy.SpaceObject model)
        {
            Console.WriteLine("PLANET VIEW " + model.spaceobject_name);
            _model = model;
            model.addEventListener(Event.CHANGE, onNextTick);
            this.moveAxisToCenter();
        }

        public void onNextTick(Event e)
        {
            this.x = _model.local_x;
            this.y = _model.local_y;
            this.rotationZ = _model.local_angle;
        }
    }
}