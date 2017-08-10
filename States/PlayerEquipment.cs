using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using Game.GCore;
using Game.Ships;
namespace Game
{
    public class PlayerEquipment : State
    {
        Player player;
        Ships.HullView hv;
        Ships.EngineInfoView ev;
        List<Ships.CannonInfoView> cannons = new List<CannonInfoView>();
        public override void init()
        {
            player = (Player)args["player"];
            
            UI.Plane bg = new UI.Plane();
            bg.x = Camera.sw / 2;
            bg.y = Camera.sh / 2;
            bg.load("DATA\\Other\\bg-black-70p.png");
            bg.width = 800;
            bg.ratio = false;
            bg.height = 500;
            bg.moveAxisToCenter();
            keeper.add(Game.interfaceView, bg);

            ev = new Ships.EngineInfoView(player.ship.getEngine());
            ev.x = Camera.sw / 2 + 50;
            ev.y = Camera.sh / 2 - 150;
            keeper.add(Game.interfaceView, ev);

            hv = new Ships.HullView(player.ship.getHull());
            hv.rotationZ = (float)Math.PI / 2;
            hv.width = 300;
            hv.x = Camera.sw / 2 - 150;
            hv.y = Camera.sh / 2 ;
            keeper.add(Game.interfaceView, hv);

            int xc = Camera.sw / 2 + 50;
            int yc = Camera.sh / 2 - 150 + 20;
            for (int i = 0; i < player.ship.cannons.Count; i++)
            {
                CannonInfoView c = new CannonInfoView(player.ship.cannons[i]);
                cannons.Add(c);
                c.x = xc;
                c.y = yc;
                yc += 100;
                keeper.add(Game.interfaceView, c);
            }

            keeper.add(Game.keyboard, KeyboardEvent.KEY_UP, onKeyUp);

        }
        private void onKeyUp(Event e)
        {
            KeyboardEvent ev=(KeyboardEvent)e;
            if (ev.keyCode == Keyboard.ESCAPE)
            {
                dispatchEvent(new StateEvent(this, StateEvent.TURN_OFF));
            }
        }
        public override void release()
        {
            keeper.clearAll();
        }
    }
}
