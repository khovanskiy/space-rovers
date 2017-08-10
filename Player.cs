using System;

namespace Game
{
    public class Player : RegistrySystem.RegistryObject
    {
        public Ships.Ship ship;
        public GCore.Point target;

        public Player()
        {
            ship = new Ships.Ship();
            target = new GCore.Point(0, 0);
        }

        public void nextTick()
        {
            ship.turnToPoint(target.x, target.y);
        }

        public void nextStep()
        {
            Random rand = new Random();
            target.x = rand.Next(-500, 500);
            target.y = rand.Next(-500, 500);
        }
    }
}