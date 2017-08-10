using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Ships
{
    public class Cannon : Component
    {
        public int damage = 0;
        public int radius = 0;
        public int rate = 0;

        public int ddamage = 0;
        public int dradius = 0;
        public int drate = 0;

        override public Component likeIt()
        {
            Cannon copy = new Cannon();
            copy.damage = this.damage;
            copy.radius = this.radius;
            copy.rate = this.rate;
            copy.type = this.type;
            return copy;
        }
    }
}
