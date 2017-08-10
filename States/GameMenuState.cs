using System.Windows.Forms;
using Game.GCore;

namespace Game.States
{
    public class GameMenuState : State
    {
        UITemplate ui;
        TextField tf = new TextField("");

        override public void init()
        {
            ui = new UITemplate(Resource.getXml("UI\\Templates\\main_menu.xml"));
            keeper.add(ui, MouseEvent.CLICK, onClick);
            keeper.add(Game.mouse, MouseEvent.MOUSE_MOVE, onMove);
            keeper.add(Game.interfaceView, ui.layout);
            keeper.add(Game.interfaceView, tf);
            tf.x = 600;
            ui.layout.x = 150;
        }

        private void onMove(Event e)
        {
            MouseEvent ev = (MouseEvent) e;
            tf.text = "Move " + Cursor.Position.X + " " + Cursor.Position.Y;
        }

        private void onClick(Event e)
        {
            switch (e.target.id)
            {
                case "new_game":
                {
                    dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "new_game"));
                }
                    break;
                case "options":
                {
                    dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "options"));
                }
                    break;
                case "exit":
                {
                    dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "exit"));
                }
                    break;
            }
        }

        public override void release()
        {
            keeper.clearAll();
            ui.clear();
        }
    }
}