using Game.GCore;

namespace Game
{
    public class SpaceSystemView : Sprite
    {
        private Astronomy.SpaceObject _model;

        public SpaceSystemView(Astronomy.SpaceObject model)
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