using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Ships
{
    public class Hull : Component
    {
        public float protection;
        public int capacity;
        public int number_of_cannons;
        public int number_of_free_slots;

        override public Component likeIt()
        {
            Hull copy = new Hull();
            copy.protection = this.protection;
            copy.capacity = this.capacity;
            copy.number_of_cannons = this.number_of_cannons;
            copy.number_of_free_slots = this.number_of_free_slots;
            copy.type = this.type;
            return copy;
        }
    }
}
