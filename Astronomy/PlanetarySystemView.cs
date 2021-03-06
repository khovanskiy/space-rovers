﻿using Astronomy;
using Game.Astronomy;
using Game.Astronomy.Objects;
using Game.GCore;

namespace Game
{
    public class PlanetarySystemView : Sprite
    {
        PlanetarySystem planerarySystemModel;

        public PlanetarySystemView(PlanetarySystem so)
        {
            this.planerarySystemModel = so;
            rec(this, so);
        }

        private void rec(DisplayObjectContainer parent, SpaceObject so)
        {
            for (int i = 0; i < so.links.Count; i++)
            {
                SpaceObject child = (SpaceObject) RegistrySystem.Registry.getInstance().getElement(so.links[i]);
                if (child.spaceobject_type != SpaceObject.SpaceSystem)
                {
                    PlanetView b = new PlanetView(child);
                    b.load(child.src);
                    b.setScaleXY(child.size);
                    b.setRSPointToCenter();
                    parent.addChild(b);
                }
                else
                {
                    SpaceSystemView d = new SpaceSystemView(child);
                    parent.addChild(d);
                    rec(d, child);
                }
            }
        }
    }
}