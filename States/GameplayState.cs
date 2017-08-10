using System;
using System.Collections.Generic;
using Game.GCore;
using Astronomy;
using RegistrySystem;
using System.Collections;

namespace Game
{
    public class GameplayState : State
    {
        PlanetarySystemView current_ps;
        TextField console = new TextField("");
        GCore.Timer timer;
        bool game_run = false;
        bool linkCameraToPlayer = true;
        private List<AIController> con = new List<AIController>();
        private AIController player;
        SpaceObject universe;

        override public void init()
        {
            universe = (SpaceObject) Registry.getInstance().getElement("universe");
            timer = new GCore.Timer();
            timer.interval = 3000;
            timer.stop();

            game_run = false;

            keeper.add(Game.interfaceView, console);
            console.text = "init";

            PlanetarySystem ss = (PlanetarySystem) Registry.getInstance().getElement("s0000001");
            current_ps = new PlanetarySystemView(ss);

            keeper.add(Game.stage, current_ps);
            ss.nextTick(0.0001f);
            Bitmap bg = new Bitmap();
            bg.load("DATA\\Backgrounds\\1.jpg");
            keeper.add(Game.background, bg);

            Game.camera.scaleX = Game.camera.scaleY = 1f;

            Player mplayer = (Player) Registry.getInstance().getElement("player");
            mplayer.ship.setHull(Economy.hulls[0]);
            mplayer.ship.setEngine(Economy.engines[0]);
            player = new AIController(mplayer);
            PlayerSpaceView view = new PlayerSpaceView(mplayer, current_ps);
            view.rotationZ = (float) Math.PI / 2;

            for (int i = 0; i < 200; i++)
            {
                Player r = createRandomPlayer();
                AIController aic = new AIController(r);
                PlayerSpaceView view2 = new PlayerSpaceView(r, current_ps);
                keeper.add(view2, MouseEvent.CLICK, clickAtShip);
                con.Add(aic);
                GameWorld.players.Add(r);
            }

            keeper.add(Game.mouse, MouseEvent.CLICK, onClick);
            keeper.add(Game.mouse, MouseEvent.MOUSE_WHEEL, onWheel);
            keeper.add(Game.mouse, MouseEvent.MOUSE_MOVE, onMove);
            keeper.add(Game.keyboard, KeyboardEvent.KEY_UP, onKeyUp);
            keeper.add(timer, TimerEvent.TIMER, onTimer);
        }

        private void clickAtShip(Event e)
        {
            var table = new Hashtable();
            table["player"] = ((PlayerSpaceView) e.target).player;
            dispatchEvent(new StateEvent(this, StateEvent.TURN_ON, "equipment", table));
        }

        private Player createRandomPlayer()
        {
            Player player = new Player();
            int id_hull = Game.random.Next(0, Economy.hulls.Count);
            player.id_frac = id_hull;
            player.ship.setHull((Ships.Hull) Economy.hulls[id_hull].likeIt());
            player.ship.setEngine((Ships.Engine) Economy.engines[Game.random.Next(0, Economy.engines.Count)].likeIt());
            for (int j = 0; j < player.ship.getHull().number_of_cannons + 1; j++)
            {
                player.ship.addComponent((Ships.Cannon) Economy.cannons[Game.random.Next(0, Economy.cannons.Count)]
                    .likeIt());
            }
            return player;
        }

        private void onTimer(Event e)
        {
            game_run = false;
            timer.stop();
        }

        public override void focus()
        {
            setRendering(true);
            keeper.resumeListeners();
        }

        private void onMove(Event e)
        {
        }

        private void onKeyUp(Event e)
        {
            KeyboardEvent evt = (KeyboardEvent) e;
            if (evt.keyCode == Keyboard.ESCAPE)
            {
                dispatchEvent(new StateEvent(this, StateEvent.TURN_ON, "pause"));
            }
            if (evt.keyCode == Keyboard.F5)
            {
                dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "generate"));
            }
            if (evt.keyCode == Keyboard.C)
            {
                linkCameraToPlayer = true;
            }
            if (evt.keyCode == Keyboard.SPACE)
            {
                game_run = true;
                makeGlobalStep();
                timer.start();
            }
        }

        private void makeGlobalStep()
        {
            for (int i = 0; i < con.Count; i++)
            {
                con[i].nextStep(GameWorld.players);
            }
        }

        private void onWheel(Event e)
        {
            MouseEvent evt = (MouseEvent) e;
            float k = 0.5f;
            if (evt.delta > 0 && Math.Max(Game.camera.scaleX, Game.camera.scaleY) < 50)
            {
                Game.camera.scaleX += k;
                Game.camera.scaleY += k;
            }
            else if (evt.delta < 0 && Math.Min(Game.camera.scaleX, Game.camera.scaleY) > 0.5)
            {
                Game.camera.scaleX -= k;
                Game.camera.scaleY -= k;
            }
        }

        private void onClick(Event e)
        {
            if (!game_run)
            {
                MouseEvent evt = (MouseEvent) e;
                Player player = (Player) Registry.getInstance().getElement("player");
                Point g = current_ps.localToGlobal(new Point(evt.localX, evt.localY));
                player.destination.x = g.x;
                player.destination.y = g.y;
            }
        }

        override public void render()
        {
            console.text = GraphicCore.TOTAL_COUNT + "";
            if (game_run)
            {
                universe.nextTick(0.0001f);

                for (int i = 0; i < con.Count; i++)
                {
                    con[i].nextTick(0.0001f);
                    //con[i].move();
                }

                //player.nextTick(0.0001f);
            }
            float speed = 0.1f;
            if (Mouse.x <= Camera.width * 0.1f)
            {
                Game.camera.x -= speed;
                linkCameraToPlayer = false;
            }
            if (Mouse.x >= Camera.width * 0.9f)
            {
                Game.camera.x += speed;
                linkCameraToPlayer = false;
            }
            if (Mouse.y <= Camera.height * 0.1f)
            {
                Game.camera.y -= speed;
                linkCameraToPlayer = false;
            }
            if (Mouse.y >= Camera.height * 0.9f)
            {
                Game.camera.y += speed;
                linkCameraToPlayer = false;
            }
        }

        public override void defocus()
        {
            keeper.pauseListeners();
            setRendering(false);
        }

        override public void release()
        {
            keeper.clearAll();
            Game.stage.clear();
            Game.background.clear();
            Game.interfaceView.clear();
            Registry.getInstance().clear();
            Game.camera.clear();
            GC.Collect();
        }
    }
}