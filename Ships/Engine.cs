using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Ships
{
    public class Engine : Component
    {
        public int radius;
        public int dradius;
        public int power;
        public int dpower;
        override public Component likeIt()
        {
            Engine copy=new Engine();
            copy.radius = this.radius;
            copy.power = this.power;
            copy.type = this.type;
            return copy;
        }
    }
}
