using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.GCore;

namespace Game
{
    public class PlayerSpaceView : View
    {
        MovieClip hull_view = new MovieClip();
        public Player player;
        public PlayerSpaceView(Player model, DisplayObjectContainer parent)
            : base(model, parent)
        {
            this.player = model;
            player.ship.addEventListener(Event.CHANGE, handler);
            player.ship.addEventListener(Ships.ShipEvent.PART_CHANGE, handler);
            this.addChild(hull_view);
            hull_view.addEventListener(MouseEvent.CLICK, onClick);
            reloadHull();
        }
        ~PlayerSpaceView()
        {
            hull_view.removeEventListener(MouseEvent.CLICK, onClick);
            player.ship.removeEventListener(Event.CHANGE, handler);
            player.ship.removeEventListener(Ships.ShipEvent.PART_CHANGE, handler);
        }
        private void onClick(Event e)
        {
            e.target = this;
            dispatchEvent(e);
        }
        private void reloadHull()
        {
            if (player.ship.getHull() != null)
            {
                hull_view.removeEventListener(MouseEvent.CLICK, onClick);
                removeChild(hull_view);
                hull_view = new MovieClip();
                hull_view.addEventListener(MouseEvent.CLICK, onClick);
                hull_view.load(new string[] { "DATA\\Ships\\" + player.ship.getHull().type + ".png" });
                hull_view.width = 50;
                hull_view.moveAxisToCenter();
                hull_view.stop();
                hull_view.x = hull_view.x + 5;
                addChild(hull_view);
            }
        }
        public void handler(Event e)
        {
            switch (e.type)
            {
                case Event.CHANGE:
                    {
                        this.x = player.ship.pos.x;
                        this.y = player.ship.pos.y;
                        this.rotationZ = player.ship.angle;
                    } break;
                case Ships.ShipEvent.PART_CHANGE:
                    {
                        reloadHull();
                    } break;
            }
        }
    }
}
