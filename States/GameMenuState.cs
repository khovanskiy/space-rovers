using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game.GCore;

namespace Game
{
    public class GameMenuState : State
    {
        UITemplate ui;
        TextField tf = new TextField("");
        Bitmap dor = new Bitmap();
        Bitmap b = new Bitmap();
        public GameMenuState()
        {

        }
        override public void init()
        {
            ui = new UITemplate(Resource.getXml("UI\\Templates\\main_menu.xml"));
            b.load("DATA\\Other\\dot.png");
            b.moveAxisToCenter();
            dor.load("DATA\\Other\\dot.png");
            dor.moveAxisToCenter();
            keeper.add(ui, MouseEvent.CLICK, onClick);
            keeper.add(Game.mouse, MouseEvent.MOUSE_MOVE, onMove);
            keeper.add(Game.interfaceView, ui.layout);
            keeper.add(Game.interfaceView, tf);
            keeper.add(Game.interfaceView, dor);
            keeper.add(Game.interfaceView, b);
            b.y = 10;
            b.x = 600;
            tf.x = 600;
            ui.layout.x = 150;
        }
        private void onMove(Event e)
        {
            MouseEvent ev = (MouseEvent)e;
            dor.x = ev.localX;
            dor.y = ev.localY;
            tf.text = "Move " + System.Windows.Forms.Cursor.Position.X + " " + System.Windows.Forms.Cursor.Position.Y;
        }
        private void onClick(Event e)
        {
            switch (e.target.id)
            {
                case "new_game":
                    {
                        dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "new_game"));
                    } break;
                case "options":
                    {
                        dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "options"));
                    } break;
                case "exit":
                    {
                        dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "exit"));
                    } break;
            }
        }
        override public void render()
        {
            GraphicCore core = GraphicCore.getInstance();
            //SharpDX.Matrix dpi = SharpDX.Matrix.Scaling(Camera.width / 1920.0f, Camera.height / 1200.0f, 1);
            //tf.text = dpi + " " + Camera.width + " " + System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height;
        }
        override public void release()
        {
            keeper.clearAll();
            ui.clear();
        }
    }
}
