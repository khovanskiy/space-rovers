using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public class Player : Model
    {
        public Ships.Ship ship;
        public GCore.Point destination = new GCore.Point(0, 0);
        public List<Player> attack;
        public int money;
		public float[] layalty;
        public AI.PrioritySelector tasker;
        public int id_frac;
        public Player()
        {
            ship = new Ships.Ship(this);
            attack = new List<Player>();
        }
    }
}
