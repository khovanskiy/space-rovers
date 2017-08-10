using System.Collections.Generic;
using Astronomy;
using Game.Astronomy.Objects;

namespace Game.Astronomy
{
    public class PlanetarySystem : SpaceObject
    {
        public List<Player> players;

        public PlanetarySystem(string name = "")
            : base(name, SpaceSystem, "universe", new RelativeMovement())
        {
            players = new List<Player>();
        }
    }
}