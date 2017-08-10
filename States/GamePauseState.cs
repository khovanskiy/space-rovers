using Game.GCore;

namespace Game.States
{
    public class GamePauseState : State
    {
        UITemplate ui;

        override public void init()
        {
            ui = new UITemplate(Resource.getXml("UI\\Templates\\pause_menu.xml"));
            keeper.add(Game.interfaceView, ui.layout);
        }
        public override void focus()
        {
            keeper.add(ui, MouseEvent.CLICK, onClick);
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
        private void onClick(Event e)
        {
            switch (e.target.id)
            {
                case "continue":
                    {
                        dispatchEvent(new StateEvent(this, StateEvent.TURN_OFF));
                    } break;
                case "menu":
                    {
                        dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "menu"));
                    } break;
                case "exit":
                    {
                        dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "exit"));
                    }break;
            }
        }
        override public void render()
        {
            
        }
        override public void release()
        {
            ui.clear();
        }
    }
}
