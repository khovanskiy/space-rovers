using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Ships
{
    public class Ship : RegistrySystem.RegistryObject
    {
        public int type = 0;
        public float x = 0;
        public float y = 0;
        public float angle = 0;
        Hull hull;
        Engine engine;
        List<Cannon> cannons;
        public Ship()
        {
            cannons = new List<Cannon>();
        }
        public void setHull(Hull hull)
        {
            this.hull = hull;
            dispatchEvent(new ShipEvent(this, ShipEvent.PART_CHANGE));
        }
        public void turnToPoint(float x, float y)
        {
            angle = (float)(-Math.Atan2(this.x - x, this.y - y) + Math.PI / 2);
            update();
        }
        public void update()
        {
            dispatchEvent(new GCore.Event(this,GCore.Event.CHANGE));
        }
    }
}
