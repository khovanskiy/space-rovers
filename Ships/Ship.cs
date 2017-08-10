using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Ships
{
    public class Ship : RegistrySystem.RegistryObject
    {
        public int type = 0;
        public GCore.Point pos = new GCore.Point(0, 0);
        public float angle = 0;
        Hull hull;
        Engine engine;
        public List<Cannon> cannons;
        List<Component> hold;
        public Player owner;
        public Ship(Player owner)
        {
            this.owner = owner;
            cannons = new List<Cannon>();
            hold = new List<Component>();
        }
        public List<Component> getHold()
        {
            return hold;
        }
        public Cannon findBestCannon()
        {
            float max = Single.MinValue;
            Cannon best = null;
            for (int i = 0; i < hold.Count; i++)
            {
                if (hold[i] is Cannon)
                {
                    if (hold[i].score > max)
                    {
                        max = hold[i].score;
                        best = (Cannon)hold[i];
                    }
                }
            }
            return best;
        }
        public Engine findBestEngine()
        {
            float max = Single.MinValue;
            Engine best = null;
            for (int i = 0; i < hold.Count; i++)
            {
                if (hold[i] is Engine)
                {
                    if (hold[i].score > max)
                    {
                        max = hold[i].score;
                        best = (Engine)hold[i];
                    }
                }
            }
            return best;
        }
        public bool isDestinationReached(GCore.Point destination, float dt)
        {
            return destination.distance(this.pos) <= 1.1 * engine.power * dt;
        }
        public void shot(Player target)
        {
            ShotView v = new ShotView(cannons[0], this.pos, target.ship.pos);
            Game.stage.addChild(v);
        }
        public void moveForward(float dt)
        {
            pos.x -= (float)Math.Cos(this.angle) * engine.power * dt;
            pos.y -= (float)Math.Sin(this.angle) * engine.power * dt;
            update();
        }
        public Engine getEngine()
        {
            return this.engine;
        }
        public Hull getHull()
        {
            return this.hull;
        }
        public void setEngine(Engine engine)
        {
            this.engine = engine;
            dispatchEvent(new ShipEvent(this, ShipEvent.PART_CHANGE));
        }
        public void setHull(Hull hull)
        {
            this.hull = hull;
            dispatchEvent(new ShipEvent(this, ShipEvent.PART_CHANGE));
        }
        public void addCannon(Cannon cannon)
        {
            cannons.Add(cannon);
        }
        public void removeCannon(Cannon cannon)
        {
            cannons.Remove(cannon);
        }
        public void addComponent(Component item)
        {
            hold.Add(item);
        }
        public void removeComponent(Component item)
        {
            hold.Remove(item);
        }
        public bool hasEngine()
        {
            return engine != null;
        }
        public bool hasHull()
        {
            return hull != null;
        }
        public bool canMove()
        {
            return hasEngine();
        }
        public void turnToPoint(GCore.Point target, float dt)
        {
            float new_angle = (float)(-Math.Atan2(this.pos.x - target.x, this.pos.y - target.y) + Math.PI / 2);
            angle += (new_angle - angle) * dt * 10;
            //Console.WriteLine(angle + " " + new_angle);
            //angle += (angle - new_angle) * dt * 100;
        }
        public void update()
        {
            dispatchEvent(new GCore.Event(this, GCore.Event.CHANGE));
        }
    }
}
