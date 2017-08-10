using Game.GCore;

namespace Game
{
    public class GameMenuState : State
    {
        UITemplate ui;
        TextField tf = new TextField("");

        public GameMenuState()
        {
        }

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
            tf.text = "Move " + System.Windows.Forms.Cursor.Position.X + " " + System.Windows.Forms.Cursor.Position.Y;
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