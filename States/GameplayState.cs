using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game.GCore;
using Astronomy;
using System.Threading;

using RegistrySystem;

namespace Game
{
    public class GameplayState : State
    {
        // Bitmap m;
        PlanetarySystemView current_ps;
        Boolean t = true;
        ShipView sv;
        List<Player> enemies = new List<Player>();
        TextField console = new TextField("");
        GCore.Timer timer;
        bool game_run = false;
        public GameplayState()
        {
        }
        override public void init()
        {
            timer = new GCore.Timer();
            timer.interval = 3000;
            timer.stop();
            
            game_run = false;

            keeper.add(Game.interfaceView, console);
            console.text = "init";
            SpaceObject ss = (SpaceObject)Registry.getInstance().getElement("s0000001");
            current_ps = new PlanetarySystemView(ss);
            keeper.add(Game.stage, current_ps);
            Bitmap b = Resource.getBitmap(ss.src);
            keeper.add(Game.background, b);

            Camera.scaleX = Camera.scaleY = 1f;

            Player player = (Player)Registry.getInstance().getElement("player");
            sv = new ShipView(player.ship);
            sv.id = "player";
            keeper.add(current_ps, sv);
            Random rand = new Random();
            for (int i = 0; i < 5; i++)
            {
                Player ai = new Player();
                ShipView ship_view = new ShipView(ai.ship);
                keeper.add(ship_view, MouseEvent.CLICK, onClickByShip);
                keeper.add(current_ps, ship_view);
                ai.ship.x = rand.Next(-100, 100);
                ai.ship.y = rand.Next(-100, 100);
                ai.ship.update();
                enemies.Add(ai);
            }

        }
        private void onTimer(Event e)
        {
            game_run = false;
            timer.stop();
        }
        private void onClickByShip(Event e)
        {
            MouseEvent ev = (MouseEvent)e;
            Bitmap f = Resource.getBitmap("DATA\\Other\\dot.png");
            Point g = current_ps.localToGlobal(new Point(ev.localX, ev.localY));
            f.x = g.x;
            f.y = g.y;
            keeper.add(current_ps, f);
        }
        public override void focus()
        {
            setRendering(true);
            keeper.add(sv, MouseEvent.CLICK, onClick);
            keeper.add(Game.mouse, MouseEvent.MOUSE_WHEEL, onWheel);
            keeper.add(Game.keyboard, KeyboardEvent.KEY_UP, onKeyUp);
            keeper.add(timer, TimerEvent.TIMER, onTimer);
        }
        private void onKeyUp(Event e)
        {
            KeyboardEvent evt = (KeyboardEvent)e;
            if (evt.keyCode == Keyboard.ESCAPE)
            {
                dispatchEvent(new StateEvent(this, StateEvent.TURN_ON, "pause"));
            }
            if (evt.keyCode == Keyboard.F5)
            {
                dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "generate"));
            }
            if (evt.keyCode == Keyboard.SPACE)
            {
                game_run = true;
                //timer.start();
                makeGlobalStep();
                console.text = "Space button " + enemies[0].target.x+" "+enemies[0].target.y;
            }
        }
        private void makeGlobalStep()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].nextStep();
            }
        }
        private void onWheel(Event e)
        {
            console.text = "wheel";
            MouseEvent evt = (MouseEvent)e;
            float k = 0.5f;
            if (evt.delta > 0 && Math.Max(Camera.scaleX, Camera.scaleY) < 50)
            {
                Camera.scaleX += k;
                Camera.scaleY += k;
            }
            else if (evt.delta < 0 && Math.Min(Camera.scaleX, Camera.scaleY) > 0.5)
            {
                Camera.scaleX -= k;
                Camera.scaleY -= k;
            }
        }
        private void onClick(Event e)
        {
            MouseEvent evt = (MouseEvent)e;
            Player player = (Player)Registry.getInstance().getElement("player");
            Point g = current_ps.localToGlobal(new Point(evt.localX, evt.localY));
            player.target.x = g.x;
            player.target.y = g.y;
        }
        override public void render()
        {
            if (game_run)
            {
                SpaceObject v = (SpaceObject)Registry.getInstance().getElement("universe");
                v.nextTick(0.0001f);
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].nextTick();
                }
            }
        }
        public override void defocus()
        {
            keeper.clearListeners();
            setRendering(false);
        }
        override public void release()
        {
            keeper.clearAll();
            Game.stage.clear();
            Game.background.clear();
            Game.interfaceView.clear();
            Registry.getInstance().clear();
            Camera.clear();
            GC.Collect();
        }
    }
}
